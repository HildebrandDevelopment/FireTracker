using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HD.FireTracker.Common.Interfaces.Logging
{
    public interface IFireTrackerLogger
    {
        void LogStartRecurringJobProcess(string TaskManagerProcessId, string RecurringJobName, int DaysToCleanup);
        void LogEndRecurringJobProcess(string TaskManagerProcessId);
        void LogRecurringJobProcessInfo(string TaskManagerProcessId, string Message);

    }
}
