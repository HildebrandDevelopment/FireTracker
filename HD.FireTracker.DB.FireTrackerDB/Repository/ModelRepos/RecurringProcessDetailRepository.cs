using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HD.FireTracker.DB.FireTrackerDB.Repository.Base;
using HD.FireTracker.Data.Common.Models.FireTrackerDB;
using HD.FireTracker.Data.Common.IRepositories.FireTrackerDB.ModelRepo;

namespace HD.FireTracker.DB.FireTrackerDB.Repository.ModelRepos
{
    public class RecurringProcessDetailRepository : Repository<RecurringProcessDetail>, IRecurringProcessDetailRepository
    {
        public RecurringProcessDetailRepository(FireTrackerDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<RecurringProcessDetail> GetTaskManagerProcessIdDetails(string taskManagerProcessId)
        {
            return this._dbContext.RecurringProcessDetail.Where(r => r.TaskManagerProcessId == taskManagerProcessId).AsEnumerable<RecurringProcessDetail>();
        }
    }//end class


}//end namespace
