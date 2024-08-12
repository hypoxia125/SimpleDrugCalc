using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDrugCalc.Classes
{
    public abstract class Measurement
    {
        public double Value { get; protected set; }
        public Enum Unit { get; protected set; }

        protected abstract Dictionary<Enum, double> UnitPrefixes {  get; }

        // Constructor
        public Measurement(double value, Enum unit)
        {
            Value = value;
            Unit = unit;
        }
        
        // Methods
        public void ConvertTo(Enum targetUnit)
        {
            if (Unit == targetUnit) return;

            if (!UnitPrefixes.ContainsKey(targetUnit))
            {
                throw new ArgumentException($"{this}: Invalid unit provided: {targetUnit}");
            }

            double targetFactor = UnitPrefixes[targetUnit];
            double baseValue = Value * UnitPrefixes[Unit];
            Value = baseValue / targetFactor;
            Unit = targetUnit;

            Debug.WriteLine($"{this}:: converted to: {Value}{Unit}");
        }
    }
}
