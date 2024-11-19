using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HD.FireTracker.Common.Extensions;

namespace HD.FireTracker.Common.Classes.CustomConfig
{
    /// <summary>
    /// Setup for custom appsettings.config section
    /// </summary>
    public class FireTrackerRecurringJobSettings : Dictionary<string, RecurringJobSettings>
    {
        public FireTrackerRecurringJobSettings()
        {
            
        }

        public string GetRecurringJobDefaultQueue(string RecurringJobName)
        {
            if (this.ContainsKey(RecurringJobName))
            {
                return this[RecurringJobName].RecurringJobDefaultQueue.GetNonNullValue();

            }
            else
            {
                return string.Empty;
            }
            
        }

        public string GetCronSchedule(string RecurringJobName)
        {
            if (this.ContainsKey(RecurringJobName))
            {
                return this[RecurringJobName].CronSchedule.GetNonNullValue();

            }
            else
            {
                return string.Empty;
            }

        }

        public int? GetDaysToLogCleanup(string RecurringJobName)
        {
            if (this.ContainsKey(RecurringJobName))
            {
                return this[RecurringJobName].DaysToLogCleanup;

            }
            else
            {
                return null;
            }

        }


        public bool? GetUseRunBlockCheck(string RecurringJobName)
        {
            if (this.ContainsKey(RecurringJobName))
            {
                return this[RecurringJobName].UseRunBlockCheck;

            }
            else
            {
                return null;
            }

        }
    }//end class

}
