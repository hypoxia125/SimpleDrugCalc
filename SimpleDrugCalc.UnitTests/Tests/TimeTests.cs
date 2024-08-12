using Xunit;
using SimpleDrugCalc.Classes;

namespace SimpleDrugCalc.UnitTests.Tests
{
    public class TimeTests
    {
        [Fact]
        public void Constructor_ShouldSetValueAndUnit()
        {
            // Arrange
            double value = 1.5;
            UnitsTime unit = UnitsTime.hr;

            // Act
            var time = new Time(value, unit);

            // Assert
            Assert.Equal(value, time.Value); // Check if the value is correctly assigned
            Assert.Equal(unit, time.Unit);   // Check if the unit is correctly assigned
        }
    }
}
