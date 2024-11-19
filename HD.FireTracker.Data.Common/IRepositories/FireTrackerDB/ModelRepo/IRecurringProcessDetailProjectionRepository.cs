using HD.FireTracker.Data.Common.Models.FireTrackerDB;
using HD.FireTracker.Data.Common.IRepositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HD.FireTracker.Data.Common.IRepositories.FireTrackerDB.ModelRepo
{
    public interface IRecurringProcessDetailProjectionRepository : IRepositoryReadOnly<RecurringProcessDetailProjection>
    {


        IEnumerable<RecurringProcessDetailProjection> GetAllRecurringProcessDetailProjections();

        IEnumerable<RecurringProcessDetailProjection> GetTaskManagerProcessIdDetails(string TaskManagerProcessId);
    
    }
}
