using Xunit;
using SimpleDrugCalc.Classes;
using System.Diagnostics;
using Xunit.Abstractions;

namespace SimpleDrugCalc.UnitTests.Tests
{
    public class DrugTests
    {
        private readonly ITestOutputHelper output;

        public DrugTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ConvertDrugMass_ShouldUpdateConcentration()
        {
            // Arrange
            var mass = new Mass(100, UnitsMass.mg);
            var volume = new Volume(50, UnitsVolume.ml);
            var desiredDose = new Mass(10, UnitsMass.mg);
            var drug = new Drug(mass, volume, desiredDose);

            // Act
            drug.ConvertDrugMass(UnitsMass.g);

            // Assert
            Assert.Equal(0.1, drug.DrugMass.Value); // 100mg -> 0.1g
            Assert.Equal(0.01, drug.DesiredDose.Value);
            Assert.Equal(0.002, drug.DrugConcentration); // 0.1g / 50ml = 0.002g/ml = 2mg/ml
        }

        [Fact]
        public void ConvertDrugVolume_ShouldUpdateConcentration()
        {
            // Arrange
            var mass = new Mass(100, UnitsMass.mg);
            var volume = new Volume(50, UnitsVolume.ml);
            var desiredDose = new Mass(10, UnitsMass.mg);
            var drug = new Drug(mass, volume, desiredDose);

            // Act
            drug.ConvertDrugVolume(UnitsVolume.l);

            // Assert
            Assert.Equal(0.05, drug.DrugVolume.Value); // 50ml -> 0.05l
            Assert.Equal(2000, drug.DrugConcentration); // 100mg / 0.05l = 2000mg/l
        }
        [Fact]
        public void GetCalculatedDose_ShouldReturnCorrectDose()
        {
            // Arrange
            var mass = new Mass(100, UnitsMass.mg);
            var volume = new Volume(50, UnitsVolume.ml);
            var time = new Time(10, UnitsTime.min);
            var desiredDose = new Mass(10, UnitsMass.mg);
            var drug = new Drug(mass, volume, desiredDose);
            var patient = new Patient(new Mass(70, UnitsMass.lb));

            output.WriteLine($"Patient Weight: {patient.Weight.Value} {patient.Weight.Unit}");

            // Act
            var dose = drug.GetCalculatedDose();
            var weightedDose = drug.GetCalculatedDose(patient);

            // Assert
            Assert.Equal(10, dose);
            Assert.Equal(10 * patient.Weight.Value, weightedDose);

            output.WriteLine($"Dose: {dose} | WeightedDose: {weightedDose}");
        }
        [Fact]
        public void GetCalculatedBolus_ShouldReturnCorrectBolus()
        {
            // Arrange
            var mass = new Mass(100, UnitsMass.mg);
            var volume = new Volume(50, UnitsVolume.ml);
            var time = new Time(10, UnitsTime.min);
            var desiredDose = new Mass(10, UnitsMass.mg);
            var drug = new Drug(mass, volume, desiredDose);
            var patient = new Patient(new Mass(70, UnitsMass.kg));

            output.WriteLine($"Patient Weight: {patient.Weight.Value} {patient.Weight.Unit}");

            // Act
            var bolus = drug.GetCalculatedBolus();
            var weightedBolus = drug.GetCalculatedBolus(patient);

            // Assert
            Assert.Equal(5, bolus);
            Assert.Equal(5 * patient.Weight.Value, weightedBolus);

            output.WriteLine($"Dose: {bolus} | WeightedDose: {weightedBolus}");
        }
    }
}
