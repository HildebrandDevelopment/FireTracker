using Microsoft.AspNetCore.Mvc;
using HD.FireTracker.Data.Service.Interfaces.IServices.Repository.FireTrackerDB;
using HD.FireTracker.Common.DTO.DomainObjects;

namespace HD.FireTracker.Web.Controllers
{
    public class RecurringProcessController : Controller
    {
        private readonly IRecurringProcessService _service;
        public RecurringProcessController(IRecurringProcessService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));


        }

        public IActionResult Index(string id)
        {
            List<RecurringProcessDTO> dtos = null;

            if (!string.IsNullOrEmpty(id))
            {
                dtos = _service.GetRecurringJobNameProcesses(id).ToList();
            }
            else
            {
                dtos = _service.GetLatestProcesses(200).ToList();
            }

            if (dtos == null) 
            {
                dtos = new List<RecurringProcessDTO>();
            }

            return View(dtos);
        }
    }
}
