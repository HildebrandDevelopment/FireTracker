using HD.FireTracker.Web.AppCode.MyRecurringJobProjects;
using HD.FireTracker.Common.Extensions;
using Hangfire;

namespace HD.FireTracker.Web.AppCode.RecurringJobCommon
{
   
    public static class RecurringJobScheduler
    {

        public static List<RecurringJobProjectBase> GetRecurringJobList()
        {
            List<RecurringJobProjectBase> recurringJobs = new List<RecurringJobProjectBase>();

            //add Recurring Jobs to list here --------------------------------
            recurringJobs.Add(new FireTrackerRecurringJobExample());
            recurringJobs.Add(new FireTrackerLogCleanup());
            //----------------------------------------------------------------
            return recurringJobs;
        }

    }//end class
}//end namespace
