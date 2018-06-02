using System;
using System.Collections.Generic;
using System.Text;

namespace BookTracker.Logic
{
    public static class ParseExtensions
    {
        public static int? TryParseInt(string toParse, int? defaultValue = null)
        {
            int outValue;
            if(int.TryParse(toParse, out outValue))
            {
                return outValue;
            }

            return defaultValue;
        }

        public static double? TryParseDouble(string toParse, double? defaultValue = null)
        {
            double outValue;
            if (double.TryParse(toParse, out outValue))
            {
                return outValue;
            }

            return defaultValue;
        }

        public static DateTime? TryParseDateTime(string toParse, DateTime? defaultValue = null)
        {
            DateTime outValue;
            if (DateTime.TryParse(toParse, out outValue))
            {
                return outValue;
            }

            return defaultValue;
        }
    }
}
