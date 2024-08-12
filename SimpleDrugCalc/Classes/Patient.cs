using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDrugCalc.Classes
{
    public class Patient
    {
        public Mass Weight { get; private set; }

        public Patient(Mass weight)
        {
            weight.ConvertTo(UnitsMass.kg);
            Weight = weight;
        }
    }
}
