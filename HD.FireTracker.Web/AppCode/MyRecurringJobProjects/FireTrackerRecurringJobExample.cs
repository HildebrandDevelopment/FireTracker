using HD.FireTracker.Common.Classes.CustomConfig;
using HD.FireTracker.Common.Interfaces.Logging;
using HD.FireTracker.Web.AppCode.DefaultImplementation;
using HD.FireTracker.Web.AppCode.RecurringJobCommon;
using Microsoft.Extensions.Options;

namespace HD.FireTracker.Web.AppCode.MyRecurringJobProjects
{
    [LogEverything]
    public class FireTrackerRecurringJobExample : RecurringJobCommon.RecurringJobProjectBase
    {
        public FireTrackerRecurringJobExample()
        {
        }

       
        public FireTrackerRecurringJobExample(IFireTrackerLogger fireTrackerLogger, IRecurringJobConfigSettings appSettings)
        {
            this._logger = fireTrackerLogger;
            this._appSettings = appSettings;
           
        }


        /// <summary>
        /// Must implement to schedule job
        /// </summary>
        /// <returns></returns>
        public override object ScheduleRecurringJobRun(string taskManagerProjectId)
        {
            var retVal = Hangfire.BackgroundJob.Enqueue(() => ExampleJob(taskManagerProjectId) );

            return retVal;
        }
            
        public string ExampleJob(string taskMgrProjectId)
        {
            
            this._logger.LogRecurringJobProcessInfo(taskMgrProjectId, "Log message from ExampleJob():  " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            return "ExampleJob Finished.";
        }
    }


}
