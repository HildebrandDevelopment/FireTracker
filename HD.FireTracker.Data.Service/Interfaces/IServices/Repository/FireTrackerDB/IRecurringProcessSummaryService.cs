using HD.FireTracker.Data.Common.Models.FireTrackerDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HD.FireTracker.Common.DTO.DomainObjects;

namespace HD.FireTracker.Data.Service.Interfaces.IServices.Repository.FireTrackerDB
{
    public interface IRecurringProcessSummaryService
    {
        IEnumerable<RecurringProcessSummaryDTO> GetAllRecurringProcessSummaries();
    }
}
