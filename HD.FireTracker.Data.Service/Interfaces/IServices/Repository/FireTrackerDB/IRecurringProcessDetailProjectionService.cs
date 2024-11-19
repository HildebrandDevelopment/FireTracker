using HD.FireTracker.Data.Common.Models.FireTrackerDB;
using HD.FireTracker.Common.DTO.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HD.FireTracker.Data.Service.Interfaces.IServices.Repository.FireTrackerDB
{
    public interface IRecurringProcessDetailProjectionService
    {

        IEnumerable<RecurringProcessDetailProjectionDTO> GetAllRecurringProcessDetailProjections();

        IEnumerable<RecurringProcessDetailProjectionDTO> GetTaskManagerProcessIdDetails(string TaskManagerProcessId);



    }
}
