using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HD.FireTracker.Common.Classes.CustomConfig
{
    public class RecurringJobSettings
    {
        
        public string RecurringJobDefaultQueue { get; set; } = "";

        public string CronSchedule { get; set; } = "";

        public int? DaysToLogCleanup { get; set; }

        public bool? UseRunBlockCheck { get; set; }

    }
}
