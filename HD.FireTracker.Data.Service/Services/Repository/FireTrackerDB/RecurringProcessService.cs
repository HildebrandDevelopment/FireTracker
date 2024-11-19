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
    public class RecurringProcessService : IRecurringProcessService
    {

        private readonly IRecurringProcessRepository _repository;
        private readonly IMapper _mapper;


        public RecurringProcessService(IRecurringProcessRepository recurringProcessRepository, IMapper mapper)
        {
            _repository = recurringProcessRepository ?? throw new ArgumentNullException(nameof(recurringProcessRepository));
            _mapper = mapper ?? throw new ArgumentNullException("mapper");
        }

        public IEnumerable<RecurringProcessDTO> GetLatestProcesses(int count)
        {
            var repoResults = _repository.GetLatestProcesses(count);

            IEnumerable<RecurringProcessDTO> serviceResults = _mapper.Map<IEnumerable<RecurringProcessDTO>>(repoResults);

            return serviceResults;
        }

        public IEnumerable<RecurringProcessDTO> GetRecurringJobNameProcesses(string recurringJobName)
        {

            var repoResults = _repository.GetRecurringJobNameProcesses(recurringJobName);

            IEnumerable<RecurringProcessDTO> serviceResults = _mapper.Map<IEnumerable<RecurringProcessDTO>>(repoResults);
            
            return serviceResults;

        }
    }//end class



}
