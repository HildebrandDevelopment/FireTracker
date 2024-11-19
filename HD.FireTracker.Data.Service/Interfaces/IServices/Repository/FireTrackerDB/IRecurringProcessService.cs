using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HD.FireTracker.Common.DTO.DomainObjects;

namespace HD.FireTracker.Data.Service.Interfaces.IServices.Repository.FireTrackerDB
{
    public interface IRecurringProcessService
    {
        IEnumerable<RecurringProcessDTO> GetLatestProcesses(int count);

        IEnumerable<RecurringProcessDTO> GetRecurringJobNameProcesses(string recurringJobName);
    }


}
