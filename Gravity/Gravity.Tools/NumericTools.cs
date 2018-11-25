using System;
using System.Collections.Generic;
using System.Linq;

namespace Gravity.Tools
{
    public static class NumericTools
    {
        public static int ToInt(this string stringValue)
        {
            return int.Parse(stringValue);
        }

        public static long ToLong(this string stringValue)
        {
            return long.Parse(stringValue);
        }

        public static double ToDouble(this string stringValue)
        {
            return double.Parse(stringValue);
        }

        public static List<int> ToListInt(this string str)
        {
            return string.IsNullOrEmpty(str.Trim()) ? null : str.Split(Convert.ToChar(",")).Select(int.Parse).ToList();
        }
    }
}