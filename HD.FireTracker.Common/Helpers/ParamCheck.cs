using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HD.FireTracker.Common.Helpers
{

    /// <summary>
    /// Safety wrappers for expected values of objects
    /// </summary>
    public static class ParamCheck
    {
        public static string ParamCheckString(object value, string defaultIfNull)
        {
            if(value != null)
            {
                
                return Convert.ToString(value);
            }
            else
            {
                return defaultIfNull;
            }
        }
        public static string ParamCheckString(object value)
        {
            return ParamCheckString(value, string.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ParamCheckInt(object value)
        {
            return ParamCheckInt(value, int.MinValue);
        }

        public static int ParamCheckInt(object value, int defaultIfNull)
        {
            int returnValue;
            if (int.TryParse(ParamCheckString(value), out returnValue))
            {
                return returnValue;

            }
            else 
            { return defaultIfNull; }


        }
    }//end class
}
