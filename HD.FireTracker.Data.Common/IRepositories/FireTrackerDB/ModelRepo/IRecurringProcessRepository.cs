using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HD.FireTracker.Data.Common.Models.FireTrackerDB;
using HD.FireTracker.Data.Common.IRepositories.Base;

namespace HD.FireTracker.Data.Common.IRepositories.FireTrackerDB.ModelRepo
{
    public interface IRecurringProcessRepository : IRepository<RecurringProcess>
    {
        IEnumerable<RecurringProcess> GetLatestProcesses(int count);

        IEnumerable<RecurringProcess> GetRecurringJobNameProcesses(string recurringJobName);

        RecurringProcess GetRecurringProcess(string taskManagerProcessId);

        IEnumerable<RecurringProcess> GetRecurringProcessesForDeletion();
    }
}
