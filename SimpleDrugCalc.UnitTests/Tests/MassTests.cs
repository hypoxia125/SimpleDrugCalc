using SimpleDrugCalc.Classes;
using Xunit;

namespace SimpleDrugCalc.UnitTests.Tests
{
    public class MassTests
    {
        [Fact]
        public void Constructor_ShouldSetValueAndUnit()
        {
            // Arrange
            double value = 1.5;
            UnitsMass unit = UnitsMass.g;

            // Act
            var mass = new Mass(value, unit);

            // Assert
            Assert.Equal(value, mass.Value);
            Assert.Equal(unit, mass.Unit);
        }
    }
}