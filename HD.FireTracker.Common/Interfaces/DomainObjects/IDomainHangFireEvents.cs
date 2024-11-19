using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HD.FireTracker.Common.Interfaces.DomainObjects
{
    public interface IDomainHangFireEvents
    {
        int Id { get; set; }
        string? Message { get; set; }
        string? MessageTemplate { get; set; }
        string? Level { get; set; }
        Nullable<System.DateTime> TimeStamp { get; set; }
        string? Exception { get; set; }
        string? Properties { get; set; }
    }
}
