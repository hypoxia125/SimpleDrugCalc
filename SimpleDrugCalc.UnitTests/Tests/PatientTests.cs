using Xunit;
using SimpleDrugCalc.Classes;

namespace SimpleDrugCalc.UnitTests.Tests
{
    public class PatientTests
    {
        [Fact]
        public void Patient_Constructor_ShouldSetWeight()
        {
            // Arrange
            var weight = new Mass(70, UnitsMass.kg);

            // Act
            var patient = new Patient(weight);

            // Assert
            Assert.Equal(weight, patient.Weight); // Check if the weight is correctly assigned
        }

        [Fact]
        public void Patient_Weight_ShouldBeImmutable()
        {
            // Arrange
            var weight = new Mass(70, UnitsMass.kg);
            var patient = new Patient(weight);

            // Act
            var newWeight = new Mass(75, UnitsMass.kg);
            patient = new Patient(newWeight);

            // Assert
            Assert.Equal(75, patient.Weight.Value); // Check if the new weight is correctly assigned
            Assert.Equal(UnitsMass.kg, patient.Weight.Unit); // Check if the unit remains the same
        }
    }
}
