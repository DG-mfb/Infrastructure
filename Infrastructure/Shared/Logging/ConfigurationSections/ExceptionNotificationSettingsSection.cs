namespace Shared.Logging.ConfigurationSections
{
	using System.Configuration;
	using Kernel.Configuration;
	using Shared.Logging.ConfigurationSections;
	using Shared.Notification.ConfigurationSections;


    public class ExceptionNotificationSettingsSection : ConfigurationSectionBase
    {
        [ConfigurationProperty("emailConfiguration", IsDefaultCollection = false)]
        public EmailConfigurationElement EmailConfiguration
        {
            get
            {
                return (EmailConfigurationElement)base["emailConfiguration"];
            }
        }

        [ConfigurationProperty("exceptionNotificationSettings", IsDefaultCollection = false)]
        public ExceptionNotificationSettingsCollection ExceptionNotificationSettings
        {
            get
            {
                ExceptionNotificationSettingsCollection exceptionNotificationSettings =
                (ExceptionNotificationSettingsCollection)base["exceptionNotificationSettings"];
                return exceptionNotificationSettings;
            }
        }
    }
}