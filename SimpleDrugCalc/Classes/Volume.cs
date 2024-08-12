using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDrugCalc.Classes
{
    public class Volume : Measurement
    {
        protected override Dictionary<Enum, double> UnitPrefixes => new Dictionary<Enum, double>
        {
            { UnitsVolume.l, 1e0 },    // Base unit
            { UnitsVolume.ml, 1e-3 },
            { UnitsVolume.cc, 1e-3 },
        };

        // Constructor
        public Volume(double value, Enum unit) : base(value, unit)
        {

        }
    }
}
