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
    public class RecurringProcessSummaryService : IRecurringProcessSummaryService
    {

        private readonly IRecurringProcessSummaryRepository _repository;
        private readonly IMapper _mapper;

        public RecurringProcessSummaryService(IRecurringProcessSummaryRepository recurringProcessSummaryRepository, IMapper mapper)
        {
            _repository = recurringProcessSummaryRepository ?? throw new ArgumentNullException(nameof(recurringProcessSummaryRepository));
            _mapper = mapper ?? throw new ArgumentNullException("mapper");

        }

        public IEnumerable<RecurringProcessSummaryDTO> GetAllRecurringProcessSummaries()
        {
            var repoResults = _repository.GetAllRecurringProcessSummaries();

            IEnumerable<RecurringProcessSummaryDTO> serviceResults = _mapper.Map<IEnumerable<RecurringProcessSummaryDTO>>(repoResults);

            return serviceResults;
        }
    }
}
