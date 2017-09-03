using System.Collections.Generic;
using Kernel.Federation.MetaData;

namespace SPMetadataProvider.Metadata
{
    public class SPSSOMetadataConfiguration : MetadataConfiguration, ISPSSOMetadataConfiguration
    {
        public bool AuthnRequestsSigned { get; set; }

        public IEnumerable<IConsumerServiceContext> ConcumerServices { get; set; }
    }    
}
