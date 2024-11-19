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
    public class RecurringProcessRepository : Repository<RecurringProcess>, IRecurringProcessRepository
    {
        public RecurringProcessRepository(FireTrackerDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<RecurringProcess> GetLatestProcesses(int count)
        {
            return this._dbContext.RecurringProcess.OrderByDescending(d => d.Id).Take(count).ToList();
        }

        public IEnumerable<RecurringProcess> GetRecurringJobNameProcesses(string recurringJobName)
        {
            return this._dbContext.RecurringProcess.Where(r => r.RecurringJobName == recurringJobName).AsEnumerable<RecurringProcess>();
        }

        public RecurringProcess GetRecurringProcess(string taskManagerProcessId)
        {
            return this._dbContext.RecurringProcess.Where(r => r.TaskManagerProcessId == taskManagerProcessId).FirstOrDefault();
        }

        public IEnumerable<RecurringProcess> GetRecurringProcessesForDeletion()
        {
            DateTime dCurrent = DateTime.Now.Date;
            return this._dbContext.RecurringProcess.Where(r => r.LogCleanupDate < dCurrent).AsEnumerable<RecurringProcess>();
        }
    }//end class


}
