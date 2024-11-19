using HD.FireTracker.Common.Consts;

namespace HD.FireTracker.Web.Consts
{
    public static class ConnectionStrings
    {
        public static string HangfireConnectionString => System.Configuration.ConfigurationManager.ConnectionStrings[ConstNames.HangfireConnection].ConnectionString;
    }
}
