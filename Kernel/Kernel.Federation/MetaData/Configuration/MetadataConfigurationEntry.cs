using System.Globalization;

namespace Kernel.Federation.MetaData.Configuration
{
    public class LocalizedConfigurationEntry
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public CultureInfo Language { get; set; }
        public LocalizedConfigurationEntry()
        {
            this.Language = CultureInfo.CurrentCulture;
        }
    }
}