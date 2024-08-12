using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDrugCalc.Classes
{
    public class Time : Measurement
    {
        protected override Dictionary<Enum, double> UnitPrefixes => new Dictionary<Enum, double>
        {
            { UnitsTime.hr, 3600 },
            { UnitsTime.min, 60 },
            { UnitsTime.sec, 1 },     // Base unit
        };

        // Constructor
        public Time(double value, Enum unit) : base(value, unit)
        {

        }
    }
}
