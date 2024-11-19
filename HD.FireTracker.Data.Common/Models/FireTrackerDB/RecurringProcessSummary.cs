using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HD.FireTracker.Common.Interfaces.DomainObjects;
using HD.FireTracker.Data.Common.Interfaces;

namespace HD.FireTracker.Data.Common.Models.FireTrackerDB
{
    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public class RecurringProcessSummary : IDomainRecurringProcessSummary
    {
        public int LastId { get; set; }
        public string RecurringJobName { get; set; }
        public string Level { get; set; }
        public DateTime? LastRunTime { get; set; }
        public string Exception { get; set; }
        public string TaskManagerProcessId { get; set; }

    }
}
