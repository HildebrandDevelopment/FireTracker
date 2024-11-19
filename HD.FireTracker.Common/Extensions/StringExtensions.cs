using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HD.FireTracker.Common.Extensions
{
    public static class StringExtensions
    {
        public static string GetNonNullValue(this string value, string defaultIfNull)
        {

            if (value != null)
            {
                return value;
            }
            else
            {
                return defaultIfNull;
            }

        }

        public static string GetNonNullValue(this string value)
        {

            if (value != null)
            {
                return value;
            }
            else
            {
                return String.Empty;
            }

        }


    }//end class
}//end naemspace
