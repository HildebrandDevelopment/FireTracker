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
    public class RecurringProcessDetailProjectionRepository : RepositoryReadOnly<RecurringProcessDetailProjection>, IRecurringProcessDetailProjectionRepository
    {
        public RecurringProcessDetailProjectionRepository(FireTrackerDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<RecurringProcessDetailProjection> GetAllRecurringProcessDetailProjections()
        {
            var query = from rp in _dbContext.RecurringProcess
                        join rpd in _dbContext.RecurringProcessDetail
                        on rp.TaskManagerProcessId equals rpd.TaskManagerProcessId
                        where rp.TaskManagerProcessId != null
                        select new RecurringProcessDetailProjection
                        {
                            Id = rpd.Id,
                            RecurringJobName = rp.RecurringJobName,
                            Exception = rpd.Exception,
                            Level = rpd.Level,
                            MessageType = rpd.MessageType,
                            FireTrackerMsg = rpd.FireTrackerMsg,
                            TaskManagerProcessId = rpd.TaskManagerProcessId,
                            TimeStamp = rpd.TimeStamp
                        };

            return query.AsEnumerable<RecurringProcessDetailProjection>();

        }

        public IEnumerable<RecurringProcessDetailProjection> GetTaskManagerProcessIdDetails(string TaskManagerProcessId)
        {
            var query = from rp in _dbContext.RecurringProcess
                        join rpd in _dbContext.RecurringProcessDetail
                        on rp.TaskManagerProcessId equals rpd.TaskManagerProcessId
                        where rp.TaskManagerProcessId == TaskManagerProcessId
                        select new RecurringProcessDetailProjection
                        {
                            Id = rpd.Id,
                            RecurringJobName = rp.RecurringJobName,
                            Exception = rpd.Exception,
                            Level = rpd.Level,
                            MessageType = rpd.MessageType,
                            FireTrackerMsg = rpd.FireTrackerMsg,
                            TaskManagerProcessId = rpd.TaskManagerProcessId,
                            TimeStamp = rpd.TimeStamp
                        };

            return query.AsEnumerable<RecurringProcessDetailProjection>();

        }
    }
}
