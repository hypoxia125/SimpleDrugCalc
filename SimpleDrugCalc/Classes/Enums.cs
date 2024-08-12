using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDrugCalc.Classes
{
    public class EnumUtils
    {
        public static bool CompareEnumWithString<TEnum>(string str, TEnum enumValue) where TEnum : struct, Enum
        {
            return Enum.TryParse(str, true, out TEnum parsedEnum) &&
                    parsedEnum.Equals(enumValue);

        }
        public static TEnum? GetEnumFromString<TEnum>(string str) where TEnum : struct, Enum
        {
            if (Enum.TryParse(str, true, out TEnum parsedEnum))
            {
                return parsedEnum;
            }
            return null;
        }
    }
    public enum UnitsMass
    {
        kg,
        g,
        mg,
        mcg,
        lb,
        st
    }
    public enum UnitsVolume
    {
        l,
        ml,
        cc
    }
    public enum UnitsTime
    {
        hr,
        min,
        sec
    }
}
