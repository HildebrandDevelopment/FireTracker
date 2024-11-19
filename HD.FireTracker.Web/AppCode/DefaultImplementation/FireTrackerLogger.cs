using HD.FireTracker.Common.Interfaces.Logging;
using Serilog;

namespace HD.FireTracker.Web.AppCode.DefaultImplementation
{
    public class FireTrackerLogger : IFireTrackerLogger
    {
        public void LogStartRecurringJobProcess(string TaskManagerProcessId, string RecurringJobName, int DaysToCleanup)
        {
            Log.Information("RecurringProcess: {RecurringProcess}; TaskManagerProcessId: {TaskManagerProcessId}; RecurringJobName: {RecurringJobName}; LogCleanupDate: {LogCleanupDate}", true, TaskManagerProcessId, RecurringJobName, DateTime.Now.AddDays(DaysToCleanup));
            Log.Information("RecurringProcessDetail: {RecurringProcessDetail}; TaskManagerProcessId: {TaskManagerProcessId}; MessageType: {MessageType}", true, TaskManagerProcessId, "Start");
        }

        public void LogRecurringJobProcessInfo(string TaskManagerProcessId, string Message)
        {
            Log.Information("RecurringProcessDetail: {RecurringProcessDetail}; TaskManagerProcessId: {TaskManagerProcessId}; MessageType: {MessageType}; FireTrackerMsg: {FireTrackerMsg}", true, TaskManagerProcessId, "Detail", Message);
        }

        public void LogEndRecurringJobProcess(string TaskManagerProcessId)
        {
            Log.Information("RecurringProcessDetail: {RecurringProcessDetail}; TaskManagerProcessId: {TaskManagerProcessId}; MessageType: {MessageType}", true, TaskManagerProcessId, "End");

        }

        

        
    }
}
