using HD.FireTracker.Data.Service.Interfaces.IServices.Repository.FireTrackerDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HD.FireTracker.Data.Common.IRepositories.FireTrackerDB.ModelRepo;
using HD.FireTracker.Common.Extensions;

namespace HD.FireTracker.Data.Service.Services.Repository.FireTrackerDB
{
    public class RecurringProcessLogCleanupService : IRecurringProcessLogCleanupService
    {
        //LogCleanupDate
        private readonly IRecurringProcessRepository _repository;
        private readonly IRecurringProcessDetailRepository _recurringProcessDetailRepository;

        public RecurringProcessLogCleanupService(IRecurringProcessRepository repository, IRecurringProcessDetailRepository recurringProcessDetailRepository)
        {
            _repository = repository;
            _recurringProcessDetailRepository = recurringProcessDetailRepository;
        }

        public void RunRecurringProcessLogCleanup()
        {
            //get list of recurring processes before current date
            var recurringProcessesToDelete = _repository.GetRecurringProcessesForDeletion().ToList();

            //iterate through list
            foreach (var recurringProcess in recurringProcessesToDelete)
            {
                
                if (recurringProcess.TaskManagerProcessId != null)
                {
                   string rpProcessId = recurringProcess.TaskManagerProcessId.GetNonNullValue();
                    //get list of process details tied to current item
                    var processDetails = _recurringProcessDetailRepository.GetTaskManagerProcessIdDetails(rpProcessId);
                    if (processDetails != null)
                    {
                        var processDetailsList = processDetails.ToList();
                        //iterate through process details
                        foreach (var processDetail in processDetailsList) 
                        {
                            //delete item
                            _recurringProcessDetailRepository.DeleteEntity(processDetail);
                        }

                    }
                }

                //delete recurring process
                _repository.DeleteEntity(recurringProcess);
            }//end foreach





        }//end method
    }//end class

}//end namespace
