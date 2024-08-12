using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleDrugCalc.Classes;
using System;
using Xunit;

namespace SimpleDrugCalc.UnitTests.Tests
{
    public class UserEntryTests
    {
        [Theory]
        [InlineData("123.45", 123.45)]
        [InlineData("-123.45", 123.45)]  // Tests that negative values are converted to positive
        [InlineData("0", 0)]
        [InlineData("Invalid", -1)]      // Tests invalid input
        [InlineData("", -1)]             // Tests empty string
        public void ParseUserEntry_ShouldReturnExpectedValue(string input, double expected)
        {
            // Arrange
            var entry = new Entry { Text = input };

            // Act
            double result = UserEntry.ParseUserEntry(entry);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(new double[] {-3, -2, 0, 1, 2}, true)]
        [InlineData(new double[] { -3, -2, -1, 0, 1, 2 }, false)]
        public void ValidChecks_ShouldReturnExpectedValue(double[] input, bool expected)
        {
            // Arrange

            // Act
            bool result = UserEntry.ValidChecks(input);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}