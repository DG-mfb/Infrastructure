using System.Collections.Generic;
using Kernel.Federation.MetaData.Configuration.Cryptography;
using Kernel.Federation.MetaData.Configuration.EntityDescriptors;

namespace Kernel.Federation.MetaData.Configuration
{
    public class MetadataContext
    {
        public bool SignMetadata { get; set; }

        public ICollection<KeyDescriptorConfiguration> KeyDescriptors { get; }
        public EntityDesriptorConfiguration EntityDesriptorConfiguration { get; set; }

        public MetadataContext()
        {
            this.SignMetadata = true;
            this.KeyDescriptors = new List<KeyDescriptorConfiguration>();
        }
    }
}