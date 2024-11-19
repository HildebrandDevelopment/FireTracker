using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HD.FireTracker.Common.Interfaces.DomainObjects;


namespace HD.FireTracker.Common.DTO.DomainObjects
{
    public class RecurringProcessSummaryDTO : IDomainRecurringProcessSummary
    {
        public int LastId { get; set; }
        public string RecurringJobName { get; set; }
        public string Level { get; set; }
        public DateTime? LastRunTime { get; set; }
        public string Exception { get; set; }
        public string TaskManagerProcessId { get; set; }
    }

}
