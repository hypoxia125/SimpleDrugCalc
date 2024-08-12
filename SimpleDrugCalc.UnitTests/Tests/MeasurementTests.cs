using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleDrugCalc.Classes;

namespace SimpleDrugCalc.UnitTests.Tests
{
    public class MeasurementTests
    {
        /* 
         * Concerete Methods
         * Test using Mass subclass
         */
        [Fact]
        public void ConvertTo_ShouldConvertUnitsCorrectly()
        {
            // Arrange
            Mass mass = new Mass(100, UnitsMass.g);

            // Act
            mass.ConvertTo(UnitsMass.kg);

            // Assert
            Assert.Equal(0.1, mass.Value);
            Assert.Equal(UnitsMass.kg, mass.Unit);
        }
        [Fact]
        public void ConvertTo_InvalidUnit_ShouldThrowExeption()
        {
            // Arrange
            var mass = new Mass(100, UnitsMass.g);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => mass.ConvertTo((UnitsMass)9999));
        }
        [Fact]
        public void ConverTo_SameUnit_ShouldNotChange()
        {
            // Arrange
            var mass = new Mass(100, UnitsMass.g);

            // Act
            mass.ConvertTo(UnitsMass.g);

            // Assert
            Assert.Equal(100, mass.Value);
            Assert.Equal(UnitsMass.g, mass.Unit);
        }
    }
}
