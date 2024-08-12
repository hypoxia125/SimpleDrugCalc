using SimpleDrugCalc.Classes;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Windows.Markup;

namespace SimpleDrugCalc
{
    public partial class MainPage : ContentPage
    {
        private CancellationTokenSource debounceCancellationTokenSource = new CancellationTokenSource();
        private const int DebounceDelayMilliseconds = 500;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }
        public async void OnPageLoaded(object sender, EventArgs e)
        {
            await Task.Delay(100);
            ResetInputs();
            ResetOutputs();
        }
        private async void OnInputChanged(object sender, EventArgs e)
        {
#if DEBUG
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
#endif
            // Wait for user to stop typing like a mongoloid
            debounceCancellationTokenSource.Cancel();
            debounceCancellationTokenSource = new CancellationTokenSource();

            var token = debounceCancellationTokenSource.Token;
            await Task.Delay(DebounceDelayMilliseconds);
            if (token.IsCancellationRequested)
            {
                ResetOutputs();
                return;
            }

            // Checkbox colors
            bool isWeightBased = CheckboxWeightEnabled.IsChecked;
            bool isInfusionBased = CheckboxInfusionEnabled.IsChecked;

            CheckboxWeightEnabled.Color = isWeightBased ? Color.FromArgb("#28A745") : Color.FromArgb("#FF4C4C");
            CheckboxInfusionEnabled.Color = isInfusionBased ? Color.FromArgb("#28A745") : Color.FromArgb("#FF4C4C");

            // Input values
            double weightValue = UserEntry.ParseUserEntry(PatientWeight);
            double drugMass = UserEntry.ParseUserEntry(ConcentrationMass);
            double drugVolume = UserEntry.ParseUserEntry(ConcentrationVolume);
            double infusionTime = UserEntry.ParseUserEntry(InfusionValue);
            double inputDose = UserEntry.ParseUserEntry(InputDose);

            // Invalid checks
            double[] doubles = new double[] { drugMass, drugVolume, inputDose };
            if (!UserEntry.ValidChecks(doubles))
            {
                Debug.WriteLine("Missing key user inputs");
                return;
            }

            // Unit inputs
            string selectedWeightUnit = PatientWeightUnit.SelectedItem.ToString();
            Enum? weightUnit = EnumUtils.GetEnumFromString<UnitsMass>(selectedWeightUnit);
                        
            string selectedDrugMassUnit = ConcentrationMassUnit.SelectedItem.ToString();
            Enum? drugMassUnit = EnumUtils.GetEnumFromString<UnitsMass>(selectedDrugMassUnit);
            
            string selectedDrugVolumeUnit = ConcentrationVolumeUnit.SelectedItem.ToString();
            Enum? drugVolumeUnit = EnumUtils.GetEnumFromString<UnitsVolume>(selectedDrugVolumeUnit);
            
            string selectedInfusionTimeUnit = InfusionUnit.SelectedItem.ToString();
            Enum? infusionTimeUnit = EnumUtils.GetEnumFromString<UnitsTime>(selectedInfusionTimeUnit);
            
            string selectedInputDoseUnit = InputDoseUnit.SelectedItem.ToString();
            Enum? inputDoseUnit = EnumUtils.GetEnumFromString<UnitsMass>(selectedInputDoseUnit);

            // Invalid checks
            Enum[] nullCheck = new Enum[] { weightUnit, drugMassUnit, drugVolumeUnit, infusionTimeUnit };
            foreach (Enum? unit in nullCheck)
            {
                if (unit == null)
                {
                    ResetOutputs();
#if DEBUG
                    stopwatch.Stop();
                    Debug.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
#endif
                    return;
                }
            }

            // Negative checks
            if (weightValue < 0) {
                weightValue = Math.Abs(weightValue);
                PatientWeight.Text = weightValue.ToString();
            }
            if (drugMass < 0) { 
                drugMass = Math.Abs(drugMass);
                ConcentrationMass.Text = drugMass.ToString();
            }
            if (drugVolume < 0) { 
                drugVolume = Math.Abs(drugVolume); 
                ConcentrationVolume.Text = drugVolume.ToString();
            }
            if (infusionTime < 0) { 
                infusionTime = Math.Abs(infusionTime);
                InfusionValue.Text = infusionTime.ToString();
            }

            // Build patient and drug
            Patient patient = new Patient(new Mass(weightValue, weightUnit));
            Drug drug = new Drug(new Mass(drugMass, drugMassUnit), new Volume(drugVolume, drugVolumeUnit), new Mass(inputDose, inputDoseUnit));
            Time time = new Time(infusionTime, infusionTimeUnit);

            Debug.WriteLine($"Drug Input Dose: {drug.DesiredDose.Value}");

            // Dose and volume bolus outputs
            double value;
            value = UpdateDoseOutput(patient, drug, UnitsMass.g, UnitsVolume.ml, isWeightBased);
            Debug.WriteLine($"Value calculated for gram output: {value}");
            OutputDose_g.Text = value.ToString("F1");

            value = UpdateDoseOutput(patient, drug, UnitsMass.mg, UnitsVolume.ml, isWeightBased);
            Debug.WriteLine($"Value calculated for milligram output: {value}");
            OutputDose_mg.Text = value.ToString("F1");

            value = UpdateDoseOutput(patient, drug, UnitsMass.mcg, UnitsVolume.ml, isWeightBased);
            Debug.WriteLine($"Value calculated for microgram output: {value}");
            OutputDose_mcg.Text = value.ToString("F1");

            value = UpdateVolumeOutput(patient, drug, UnitsMass.mg, UnitsVolume.l, isWeightBased);
            Debug.WriteLine($"Value calculated for liter output: {value}");
            OutputVolume_l.Text = value.ToString("F1");

            value = UpdateVolumeOutput(patient, drug, UnitsMass.mg, UnitsVolume.ml, isWeightBased);
            Debug.WriteLine($"Value calculated for milliliter output: {value}");
            OutputVolume_ml.Text = value.ToString("F1");

            value = UpdateVolumeOutput(patient, drug, UnitsMass.mg, UnitsVolume.cc, isWeightBased);
            Debug.WriteLine($"Value calculated for microliter output: {value}");
            OutputVolume_cc.Text = value.ToString("F1");

            // Pump values
            if (isInfusionBased && !isWeightBased)
            {
                value = UpdatePumpOutput(patient, drug, time, UnitsMass.mg, UnitsVolume.ml, UnitsTime.min);
                OutputPumpMin.Text = value.ToString("F1");

                value = UpdatePumpOutput(patient, drug, time, UnitsMass.mg, UnitsVolume.ml, UnitsTime.hr);
                OutputPumpHour.Text = value.ToString("F1");
            }
            else
            {
                OutputPumpMin.Text = "n/a";
                OutputPumpHour.Text = "n/a";
            }


#if DEBUG
            stopwatch.Stop();
            Debug.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");
#endif
        }
        // Methods
        private void ResetInputs()
        {
            PatientWeightUnit.SelectedIndex = 0;
            ConcentrationMassUnit.SelectedIndex = 1;
            ConcentrationVolumeUnit.SelectedIndex = 1;
            InputDoseUnit.SelectedIndex = 1;
            InfusionUnit.SelectedIndex = 0;
        }
        private void ResetOutputs()
        {
            Label[] labels = { OutputDose_g, OutputDose_mg, OutputDose_mcg, OutputVolume_l, OutputVolume_ml, OutputVolume_cc, OutputPumpHour, OutputPumpMin };
            foreach (Label label in labels)
            {
                label.Text = "0.00";
            }
        }
        public static double UpdateDoseOutput(Patient patient, Drug drug, UnitsMass massUnit, UnitsVolume volumeUnit, bool isWeightBased)
        {
            drug.ConvertDrugMass(massUnit);
            drug.ConvertDrugVolume(volumeUnit);

            double dose;
            if (isWeightBased)
            {
                dose = drug.GetCalculatedDose(patient);
            }
            else
            {
                dose = drug.GetCalculatedDose();
            }

            return dose;
        }
        public static double UpdateVolumeOutput(Patient patient, Drug drug, UnitsMass massUnit, UnitsVolume volumeUnit, bool isWeightBased)
        {
            drug.ConvertDrugMass(massUnit);
            drug.ConvertDrugVolume(volumeUnit);

            double vol;
            if (isWeightBased)
            {
                vol = drug.GetCalculatedBolus(patient);
            }
            else
            {
                vol = drug.GetCalculatedBolus();
            }

            return vol;
        }
        public static double UpdatePumpOutput(Patient patient, Drug drug, Time time, UnitsMass massUnit, UnitsVolume volumeUnit, UnitsTime unitsTime)
        {
            drug.ConvertDrugMass(massUnit);
            drug.ConvertDrugVolume(volumeUnit);
            time.ConvertTo(unitsTime);

            double dose = drug.GetCalculatedDose();
            double rate = dose / drug.DrugConcentration / time.Value;

            return rate;
        }
    }
}
