using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HD.FireTracker.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RunRecurringJobController : ControllerBase
    {
        [HttpGet]
        [Route("FireTrackerRecurringJobExample")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> FireTrackerRecurringJobExample()
        {
            return Ok(Hangfire.RecurringJob.TriggerJob("FireTrackerRecurringJobExample"));
        }


        [HttpGet]
        [Route("FireTrackerLogCleanup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> FireTrackerLogCleanup()
        {
            return Ok(Hangfire.RecurringJob.TriggerJob("FireTrackerLogCleanup"));
        }

    }
}
