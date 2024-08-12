using SimpleDrugCalc.Classes;
using Xunit;

namespace SimpleDrugCalc.UnitTests.Tests
{
    public class VolumeTests
    {
        [Fact]
        public void Constructor_ShouldSetValueAndUnit()
        {
            // Arrange
            double value = 1.5;
            UnitsVolume unit = UnitsVolume.l;

            // Act
            var volume = new Volume(value, unit);

            // Assert
            Assert.Equal(value, volume.Value);
            Assert.Equal(unit, volume.Unit);
        }
    }
}