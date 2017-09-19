using System.Collections.Generic;

namespace Kernel.Federation.MetaData.Configuration.Organisation
{
    public class ContactConfiguration : LocalizedConfigurationEntry
    {
        public ICollection<ContactPerson> PersonContact { get; set; }
    }
}