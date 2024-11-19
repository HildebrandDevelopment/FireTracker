using Microsoft.AspNetCore.Mvc;
using HD.FireTracker.Data.Service.Interfaces.IServices.Repository.FireTrackerDB;
using HD.FireTracker.Common.DTO.DomainObjects;

namespace HD.FireTracker.Web.Controllers
{
    public class RecurringProcessDetailController : Controller
    {
        private readonly IRecurringProcessDetailProjectionService _service;

        public RecurringProcessDetailController(IRecurringProcessDetailProjectionService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public IActionResult Index(string id)
        {
            List<RecurringProcessDetailProjectionDTO> dtos = null;

            if (!string.IsNullOrEmpty(id))
            {
                dtos = _service.GetTaskManagerProcessIdDetails(id).ToList();
            }
            

            if (dtos == null)
            {
                dtos = new List<RecurringProcessDetailProjectionDTO>();
            }

            return View(dtos);
            
        }
    }
}
