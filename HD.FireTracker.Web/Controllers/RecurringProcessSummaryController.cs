using HD.FireTracker.Common.DTO.DomainObjects;
using HD.FireTracker.Data.Service.Interfaces.IServices.Repository.FireTrackerDB;
using Microsoft.AspNetCore.Mvc;

namespace HD.FireTracker.Web.Controllers
{
    public class RecurringProcessSummaryController : Controller
    {
        private readonly IRecurringProcessSummaryService _service;

        public RecurringProcessSummaryController(IRecurringProcessSummaryService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }


        public IActionResult Index()
        {
            List<RecurringProcessSummaryDTO> dtos = null;

            dtos = _service.GetAllRecurringProcessSummaries().ToList();
            

            if (dtos == null)
            {
                dtos = new List<RecurringProcessSummaryDTO>();
            }

            return View(dtos);
        }
    }
}
