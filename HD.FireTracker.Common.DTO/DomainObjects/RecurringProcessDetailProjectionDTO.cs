using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HD.FireTracker.Common.Interfaces.DomainObjects;

namespace HD.FireTracker.Common.DTO.DomainObjects
{
    public class RecurringProcessDetailProjectionDTO : IDomainRecurringProcessDetailProjection
    {
        public int Id { get; set; }
        public string RecurringJobName { get; set; }
        public string Level { get; set; }
        public DateTime? TimeStamp { get; set; }
        public string TaskManagerProcessId { get; set; }
        public string MessageType { get; set; }
        public string FireTrackerMsg { get; set; }
        public string Exception { get; set; }
    }
}
