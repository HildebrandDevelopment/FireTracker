using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace HD.FireTracker.Web.AuthorizationFilters
{
    public class FullAccessNoAuthRequired : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}
