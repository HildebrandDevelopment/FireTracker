using HD.FireTracker.Common.Classes.CustomConfig;

namespace HD.FireTracker.Web.AppCode.RecurringJobCommon
{
    public class RecurringJobConfigSettings : IRecurringJobConfigSettings
    {
        private readonly IConfigurationSection _configSection;

        public RecurringJobConfigSettings(IConfiguration configuration)
        {            
            if (configuration.GetSection("FireTrackerRecurringJobSettings").Exists())
            {
                _configSection = configuration.GetSection("FireTrackerRecurringJobSettings");
            }
        }

        public FireTrackerRecurringJobSettings ConfigSettings 
        {
            get 
            {
                FireTrackerRecurringJobSettings fireTrackerRecurringJobSettings = null;
                try
                {
                    fireTrackerRecurringJobSettings = _configSection.Get<FireTrackerRecurringJobSettings>();

                }
                catch 
                {
                }
                return fireTrackerRecurringJobSettings;
            }
        
        }
    }
}
