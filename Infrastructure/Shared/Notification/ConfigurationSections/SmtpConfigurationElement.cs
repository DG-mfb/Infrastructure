using System;
using System.Configuration;
using Kernel.Configuration;

namespace Shared.Notification.ConfigurationSections
{
    public class SmtpConfigurationElement : AbstractConfigurationElement
    {
        [ConfigurationProperty("value", IsRequired = true)]
        public String Value
        {
            get
            {
                return (String)this["value"];
            }
            set
            {
                this["value"] = value;
            }
        }
    }
}
