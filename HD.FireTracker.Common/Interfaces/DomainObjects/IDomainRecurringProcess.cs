using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HD.FireTracker.Common.Interfaces.DomainObjects
{
    public interface IDomainRecurringProcess
    {
        int Id { get; set; }
        string? Message { get; set; }
        string? MessageTemplate { get; set; }
        string? Level { get; set; }
        Nullable<System.DateTime> TimeStamp { get; set; }
        string? Exception {  get; set; }
        string? Properties { get; set; }
        string? TaskManagerProcessId { get; set; }
        string? RecurringJobName { get; set; }
        Nullable<System.DateTime> LogCleanupDate { get; set; }


    }
}
