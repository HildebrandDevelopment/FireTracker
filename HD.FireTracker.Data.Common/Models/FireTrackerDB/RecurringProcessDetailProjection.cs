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
    public class RecurringProcessDetailProjection : IDomainRecurringProcessDetailProjection
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
