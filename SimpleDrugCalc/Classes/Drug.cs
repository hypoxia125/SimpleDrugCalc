using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDrugCalc.Classes
{
    public class Drug
    {
        public Mass DrugMass { get; private set; }
        public Volume DrugVolume { get; private set; }
        public double DrugConcentration { get; private set; }
        public Mass DesiredDose { get; private set; }

        // Constructor
        public Drug(Mass mass, Volume volume, Mass desiredDose)
        {
            DrugMass = mass;
            DrugVolume = volume;
            DesiredDose = desiredDose;
            UpdateConcentration();
        }

        // Methods
        private Drug UpdateConcentration()
        {
            DrugConcentration = DrugMass.Value / DrugVolume.Value;
            Debug.WriteLine($"{this}:: Concentration updated: {DrugMass.Value}{DrugMass.Unit} / {DrugVolume.Value}{DrugVolume.Unit}");
            Debug.WriteLine($"{this}:: {DrugConcentration}");
            return this;
        }
        public void ConvertDrugMass(Enum targetUnit)
        {
            DrugMass.ConvertTo(targetUnit);
            DesiredDose.ConvertTo(targetUnit);
            UpdateConcentration();
        }
        public void ConvertDrugVolume(Enum targetUnit)
        {
            DrugVolume.ConvertTo(targetUnit);
            UpdateConcentration();
        }
        public double GetCalculatedDose()
        {
            return DesiredDose.Value;
        }
        public double GetCalculatedDose(Patient patient)
        {
            return DesiredDose.Value * patient.Weight.Value;
        }
        public double GetCalculatedDose(Patient patient, Time time)
        {
            return DesiredDose.Value * patient.Weight.Value / time.Value;
        }
        public double GetCalculatedBolus()
        {
            return DesiredDose.Value / DrugConcentration;
        }
        public double GetCalculatedBolus(Patient patient)
        {
            return DesiredDose.Value * patient.Weight.Value / DrugConcentration;
        }
    }
}
