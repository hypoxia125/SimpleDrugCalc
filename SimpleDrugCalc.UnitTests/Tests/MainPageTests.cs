using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleDrugCalc.Classes;

namespace SimpleDrugCalc.UnitTests.Tests
{
    public class MainPageTests
    {
        [Fact]
        public void UpdateDoseOutput_ShouldOutputCorrectDose()
        {
            // Arrange
            var mass = new Mass(100, UnitsMass.mg);
            var volume = new Volume(50, UnitsVolume.ml);
            var time = new Time(10, UnitsTime.min);
            var desiredDose = new Mass(10, UnitsMass.mg);
            var drug = new Drug(mass, volume, desiredDose);
            var patient = new Patient(new Mass(70, UnitsMass.kg));

            // Act
            double dose = MainPage.UpdateDoseOutput(patient, drug, (UnitsMass)mass.Unit, (UnitsVolume)volume.Unit, false);
            double weightedDose = MainPage.UpdateDoseOutput(patient, drug, (UnitsMass)mass.Unit, (UnitsVolume)volume.Unit, true);

            Assert.Equal(10, dose);
            Assert.Equal(10 * 70, weightedDose);
        }
        [Fact]
        public void UpdateVolumeOutput_ShouldOutputCorrectDose()
        {
            // Arrange
            var mass = new Mass(100, UnitsMass.mg);
            var volume = new Volume(50, UnitsVolume.ml);
            var time = new Time(10, UnitsTime.min);
            var desiredDose = new Mass(10, UnitsMass.mg);
            var drug = new Drug(mass, volume, desiredDose);
            var patient = new Patient(new Mass(70, UnitsMass.kg));

            // Act
            double vol = MainPage.UpdateVolumeOutput(patient, drug, (UnitsMass)mass.Unit, (UnitsVolume)volume.Unit, false);
            double weightedVol = MainPage.UpdateVolumeOutput(patient, drug, (UnitsMass)mass.Unit, (UnitsVolume)volume.Unit, true);

            Assert.Equal(10 / drug.DrugConcentration, vol);
            Assert.Equal((10 * 70) / drug.DrugConcentration, weightedVol);
        }
    }
}
