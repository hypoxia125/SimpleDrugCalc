using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDrugCalc.Classes
{
    public class Mass : Measurement
    {
        protected override Dictionary<Enum, double> UnitPrefixes => new Dictionary<Enum, double>
        {
            { UnitsMass.kg, 1e3 },
            { UnitsMass.g, 1e0 },    // Base unit
            { UnitsMass.mg, 1e-3 },
            { UnitsMass.mcg, 1e-6 },
            { UnitsMass.lb, 453.59237 },
            { UnitsMass.st, 6350.29318 }
        };

        // Constructor
        public Mass(double value, Enum unit) : base(value, unit)
        {

        }
    }
}
