using System.Collections.Generic;

namespace Kernel.Federation.MetaData.Configuration.Organisation
{
    public class ContactConfiguration : MetadataConfigurationEntry
    {
        public ICollection<ContactPerson> PersonContact { get; set; }
    }
}