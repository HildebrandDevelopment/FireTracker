using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HD.FireTracker.Common.DTO.DomainObjects;
using HD.FireTracker.Data.Common.IRepositories.FireTrackerDB.ModelRepo;
using HD.FireTracker.Data.Service.Interfaces.IServices.Repository.FireTrackerDB;

namespace HD.FireTracker.Data.Service.Services.Repository.FireTrackerDB
{
    public class RecurringProcessDetailProjectionService : IRecurringProcessDetailProjectionService
    {
        private readonly IRecurringProcessDetailProjectionRepository _repository;
        private readonly IMapper _mapper;

        public RecurringProcessDetailProjectionService(IRecurringProcessDetailProjectionRepository recurringProcessDetailProjectionRepository, IMapper mapper)
        {
            _repository = recurringProcessDetailProjectionRepository ?? throw new ArgumentNullException(nameof(recurringProcessDetailProjectionRepository));
            _mapper = mapper ?? throw new ArgumentNullException("mapper");
        }

        public IEnumerable<RecurringProcessDetailProjectionDTO> GetAllRecurringProcessDetailProjections()
        {
            var repoResults = _repository.GetAllRecurringProcessDetailProjections();

            IEnumerable<RecurringProcessDetailProjectionDTO> serviceResults = _mapper.Map<IEnumerable<RecurringProcessDetailProjectionDTO>>(repoResults);

            return serviceResults;
        }

        public IEnumerable<RecurringProcessDetailProjectionDTO> GetTaskManagerProcessIdDetails(string TaskManagerProcessId)
        {
            var repoResults = _repository.GetTaskManagerProcessIdDetails(TaskManagerProcessId);
            IEnumerable<RecurringProcessDetailProjectionDTO> serviceResults = _mapper.Map<IEnumerable<RecurringProcessDetailProjectionDTO>>(repoResults);

            return serviceResults;
        }
    }//end class


}//end namespace
