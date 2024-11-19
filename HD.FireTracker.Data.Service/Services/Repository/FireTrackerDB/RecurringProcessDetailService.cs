using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HD.FireTracker.Data.Service.Interfaces.IServices.Repository.FireTrackerDB;
using HD.FireTracker.Data.Common.IRepositories.FireTrackerDB.ModelRepo;

namespace HD.FireTracker.Data.Service.Services.Repository.FireTrackerDB
{
    public class RecurringProcessDetailService : IRecurringProcessDetailService
    {
        private readonly IRecurringProcessDetailRepository _recurringProcessDetailRepository;

        public RecurringProcessDetailService(IRecurringProcessDetailRepository recurringProcessDetailRepository)
        {
            _recurringProcessDetailRepository = recurringProcessDetailRepository ?? throw new ArgumentNullException(nameof(recurringProcessDetailRepository));
        }

    }//end class


}
