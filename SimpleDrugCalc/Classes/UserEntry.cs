using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDrugCalc.Classes
{
    public class UserEntry
    {
        // Methods
        public static double ParseUserEntry(Entry entry)
        {
            if (!double.TryParse(entry.Text, out double number))
            {
                entry.Text = string.Empty;
                return -1;
            }
            if (number < 0)
            {
                return Math.Abs(number);
            }
            return number;
        }
        public static bool ValidChecks(double[] set)
        {
            foreach (double value in set)
            {
                if (value == -1)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
