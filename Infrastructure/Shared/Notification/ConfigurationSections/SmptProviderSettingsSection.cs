using System.Configuration;
using Kernel.Configuration;

namespace Shared.Notification.ConfigurationSections
{
    public class SmptProviderSettingsSection : ConfigurationSectionBase
    {
        [ConfigurationProperty("smptSettings", IsDefaultCollection = false)]
        public SmptSettingsCollections SmptSettings
        {
            get
            {
                SmptSettingsCollections smptSettings =
                (SmptSettingsCollections)base["smptSettings"];
                return smptSettings;
            }
        }
    }
}