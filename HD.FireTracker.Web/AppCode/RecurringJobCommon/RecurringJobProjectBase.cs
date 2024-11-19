using HD.FireTracker.Common.Interfaces.Logging;
using HD.FireTracker.Common.Extensions;
using HD.FireTracker.Common.Helpers;
using HD.FireTracker.Common.Classes.CustomConfig;
using System.Linq.Expressions;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Hangfire.Annotations;
using System.Reflection;
using System.Formats.Asn1;

namespace HD.FireTracker.Web.AppCode.RecurringJobCommon
{
    public abstract class RecurringJobProjectBase
    {
        protected IFireTrackerLogger _logger;
        
        protected IRecurringJobConfigSettings _appSettings;

        
        private System.Threading.CancellationTokenSource _baseCancelationTokenSource = new CancellationTokenSource();

        public System.Threading.CancellationTokenSource BaseCancelationTokenSource
        {
            get { return _baseCancelationTokenSource; }
            set { _baseCancelationTokenSource = value; }
        }

        #region "Region: Properties"
        

        #region "Region: Configuration Properties"

        public void SetRecurringJobConfigurations(IRecurringJobConfigSettings recurringJobConfigSettings)
        {
            this._appSettings = recurringJobConfigSettings;
        }

        public void SetRecurringJobConfigurations(string recurringJobDefaultQueue, string cronSchedule, int daysToLogCleanup, bool useRunBlockCheck)
        {
            //set IRecurringJobConfigSettings to null...these settings will take over
            this._appSettings = null;

            this._recurringJobDefaultQueue = recurringJobDefaultQueue;
            this._cronSchedule = cronSchedule;
            this._daysToLogCleanup = daysToLogCleanup;
            this._useRunBlockCheck = useRunBlockCheck;

        }

        private string _recurringJobDefaultQueue = "default";
        public string RecurringJobDefaultQueue
        {
            get
            {
                string retVal = _recurringJobDefaultQueue;


                if (this._appSettings != null)
                {
                    if (this._appSettings.ConfigSettings != null)
                    {
                        if (!string.IsNullOrEmpty(this._appSettings.ConfigSettings.GetRecurringJobDefaultQueue(this.GetRecurringJobName())))
                        {
                            retVal = this._appSettings.ConfigSettings.GetRecurringJobDefaultQueue(this.GetRecurringJobName());
                        }
                    }
                }
                return retVal;

            }
        }

        private string _cronSchedule = Hangfire.Cron.Never();

        public string CronSchedule
        {
            get
            {
                string retVal = _cronSchedule;


                if (this._appSettings != null)
                {
                    if (this._appSettings.ConfigSettings != null)
                    {
                        if (!string.IsNullOrEmpty(this._appSettings.ConfigSettings.GetCronSchedule(this.GetRecurringJobName())))
                        {
                            retVal = this._appSettings.ConfigSettings.GetCronSchedule(this.GetRecurringJobName());
                        }
                    }
                }
                return retVal;

            }
        }



        private int _daysToLogCleanup = 90;
        public int DaysToLogCleanup
        {
            get 
            {
                int retVal = _daysToLogCleanup;


                if (this._appSettings != null)
                {
                    if (this._appSettings.ConfigSettings != null)
                    {
                        if (this._appSettings.ConfigSettings.GetDaysToLogCleanup(this.GetRecurringJobName()).HasValue)
                        {
                            retVal = this._appSettings.ConfigSettings.GetDaysToLogCleanup(this.GetRecurringJobName()).Value;
                        }
                    }
                }
                return retVal;
            
            }
        }

        private bool _useRunBlockCheck = true;
        public bool UseRunBlockCheck
        {
            get 
            {
                bool retVal = _useRunBlockCheck;


                if (this._appSettings !=null)
                {
                    if (this._appSettings.ConfigSettings != null) 
                    {
                        if (this._appSettings.ConfigSettings.GetUseRunBlockCheck(this.GetRecurringJobName()).HasValue)
                        {
                            retVal = this._appSettings.ConfigSettings.GetUseRunBlockCheck(this.GetRecurringJobName()).Value;
                        }
                    }
                }
                return retVal; 
            
            }
        }
        #endregion


        private string _taskManagerProcessId = "";

        private string TaskManagerProcessId
        {
            get { return _taskManagerProcessId; }
        }

        
        #endregion

        public string ReflectedTypeName
        {
            get { return this.GetType().Name; }
        }

        


        public string GetRecurringJobName()
        {
            return GetType().Name;
        }



        /// <summary>
        /// Implement...will get called by RecurringRunScheduler.
        /// </summary>
        /// <param name="taskManagerProjectId">Pass TaskManagerProjectId generated in base class as parameter to ScheduleRecurringJobRun</param>
        /// <returns></returns>
        public abstract object ScheduleRecurringJobRun(string taskManagerProjectId);

        
        public async Task<object> RecurringRunScheduler()
        {
            //set process guid
            _taskManagerProcessId = System.Guid.NewGuid().ToString();

            object runTask = null;

            //check job block
            bool blnCanRun = true;
            //check if blocked
            if (this.UseRunBlockCheck)
            {
                if (this.IsJobProjectRunBlocked())
                {
                    blnCanRun = false; 
                }
            }

            //implemented method call
            if (blnCanRun)
            {
                //log start
                _logger.LogStartRecurringJobProcess(TaskManagerProcessId, this.GetRecurringJobName(), this.DaysToLogCleanup);

                //actually run the implemented method
                runTask = this.ScheduleRecurringJobRun(TaskManagerProcessId);

                await Task.Delay(100);
                _logger.LogRecurringJobProcessInfo(TaskManagerProcessId, ParamCheck.ParamCheckString(runTask));
                _logger.LogEndRecurringJobProcess(TaskManagerProcessId);

            }
            else
            {
                runTask = "failed";
                throw new Exception("Run Block Exception");
            }

            return runTask;
        }//end method

        
        
        public Expression<System.Func<Task>> GetTaskExpression()
        {
            return () => this.RecurringRunScheduler();
        }


        #region "Region: Job Blocking"

        public HashSet<string> GetBlockingJobTypeNames()
        {
            HashSet<string> hs = new HashSet<string>();
            hs.Add(this.ReflectedTypeName.ToLower());

            return hs;
        }



        public bool IsJobProjectRunBlocked()
        {
            bool blnIsBlocked = false;
            HashSet<string> hsBlockCheck = this.GetBlockingJobTypeNames();
            HashSet<string> hsActiveJobs = GetJobsNonEndedStateHashset();

            var jobsMatch = hsActiveJobs.Intersect(hsBlockCheck).ToList();
            if (jobsMatch.Count > 0)
            {
                blnIsBlocked = true;
            }

            return blnIsBlocked;
        }

        public HashSet<string> GetJobsNonEndedStateHashset()
        {
            List<FireTrackerJobStateDto> stateDtos = this.GetJobsNonEndedState();
            HashSet<string> hs = new HashSet<string>();

            foreach (var item in stateDtos)
            { 
                hs.Add(item.JobTypeName.ToLower());
            }
            return hs;
        }

        public List<FireTrackerJobStateDto> GetJobsNonEndedState()
        {
            List<FireTrackerJobStateDto> stateDtos = new List<FireTrackerJobStateDto>();

            Hangfire.Storage.IMonitoringApi monitoring = Hangfire.JobStorage.Current.GetMonitoringApi();

            var enqueuedJobs = monitoring.EnqueuedJobs(this.RecurringJobDefaultQueue, 0, int.MaxValue);
            var fetchedJobs = monitoring.FetchedJobs(this.RecurringJobDefaultQueue, 0, int.MaxValue);
            var scheduledJobs = monitoring.ScheduledJobs(0, int.MaxValue);
            var processingJobs = monitoring.ProcessingJobs(0, int.MaxValue);
            var failedJobs = monitoring.FailedJobs(0, int.MaxValue);

            foreach (var job in enqueuedJobs)
            {
                FireTrackerJobStateDto dto = new FireTrackerJobStateDto { JobTypeName = job.Value.Job.Type.Name, JobStateType = "EnqueuedJobs" };
                stateDtos.Add(dto);
            }

            int iCtMe = 0;
            foreach (var job in fetchedJobs)
            {
                FireTrackerJobStateDto dto = new FireTrackerJobStateDto { JobTypeName = job.Value.Job.Type.Name, JobStateType = "FetchedJobs" };

                if (dto.JobTypeName.Equals(this.ReflectedTypeName))
                { 
                    if(iCtMe > 0)
                    {
                        stateDtos.Add(dto);
                    }
                    iCtMe += 1;
                }
                else
                {
                    stateDtos.Add(dto);
                }
            }

            foreach (var job in scheduledJobs)
            {
                FireTrackerJobStateDto dto = new FireTrackerJobStateDto { JobTypeName = job.Value.Job.Type.Name, JobStateType = "ScheduledJobs" };
                stateDtos.Add(dto);
            }

            //reset iCtMe
            iCtMe = 0;
            foreach (var job in processingJobs)
            {
                FireTrackerJobStateDto dto = new FireTrackerJobStateDto { JobTypeName = job.Value.Job.Type.Name, JobStateType = "ProcessingJobs" };

                if (dto.JobTypeName.Equals(this.ReflectedTypeName))
                {
                    if (iCtMe > 0)
                    {
                        stateDtos.Add(dto);
                    }
                    iCtMe += 1;
                }
                else
                {
                    stateDtos.Add(dto);
                }
            }//end foreach

            foreach (var job in failedJobs)
            {
                FireTrackerJobStateDto dto = new FireTrackerJobStateDto { JobTypeName = job.Value.Job.Type.Name, JobStateType = "FailedJobs" };
                stateDtos.Add(dto);
            }

            return stateDtos;
        }//end method
        #endregion


    }//end class

    public class FireTrackerJobStateDto
    {
        public string JobTypeName { get; set; }

        public string JobStateType { get; set; }
    }

}//end namespace
