using HD.FireTracker.Common.Interfaces.Logging;
using HD.FireTracker.Web.AppCode.DefaultImplementation;
using Microsoft.Identity.Client;
using HD.FireTracker.Data.Service.Interfaces.IServices.Repository.FireTrackerDB;
using HD.FireTracker.Web.AppCode.RecurringJobCommon;

namespace HD.FireTracker.Web.AppCode.MyRecurringJobProjects
{

    [LogEverything]
    public class FireTrackerLogCleanup : RecurringJobCommon.RecurringJobProjectBase
    {
        private IRecurringProcessLogCleanupService _logCleanupService;

        public FireTrackerLogCleanup()
        {

        }

        public FireTrackerLogCleanup(IFireTrackerLogger fireTrackerLogger, IRecurringJobConfigSettings appSettings, IRecurringProcessLogCleanupService logCleanupService)
        {
            this._logger = fireTrackerLogger;
            this._appSettings = appSettings;
            this._logCleanupService = logCleanupService;
            
        }

        public override object ScheduleRecurringJobRun(string taskManagerProjectId)
        {
            var retVal = Hangfire.BackgroundJob.Enqueue(() => StartRecurringLogCleanup(taskManagerProjectId));

            return retVal;
        }

        public string StartRecurringLogCleanup(string taskMgrProjectId)
        {
            this._logger.LogRecurringJobProcessInfo(taskMgrProjectId, "Beginning StartRecurringLogCleanup():  " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            this._logCleanupService.RunRecurringProcessLogCleanup();
            return "Finished Recurring Process Log Cleanup: " + DateTime.Now.ToString("f");
        }
    }//end class

}//end namespace
