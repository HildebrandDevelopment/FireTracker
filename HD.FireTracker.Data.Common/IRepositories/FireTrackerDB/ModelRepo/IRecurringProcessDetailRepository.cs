using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HD.FireTracker.Data.Common.Models.FireTrackerDB;
using HD.FireTracker.Data.Common.IRepositories.Base;

namespace HD.FireTracker.Data.Common.IRepositories.FireTrackerDB.ModelRepo
{
    public interface IRecurringProcessDetailRepository : IRepository<RecurringProcessDetail>
    {
        IEnumerable<RecurringProcessDetail> GetTaskManagerProcessIdDetails(string taskManagerProcessId);

    }
}
