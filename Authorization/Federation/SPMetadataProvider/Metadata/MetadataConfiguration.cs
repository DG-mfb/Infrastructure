using System;
using System.Collections.Generic;
using Kernel.Federation.MetaData;

namespace WsFederationMetadataProvider.Metadata
{
    public class MetadataConfiguration : IMetadataConfiguration
    {
        public string MetadatFilePathDestination { get; set; }

        public bool SignMetadata { get; set; }

        public Uri EntityId { get; set; }

        public string DescriptorId { get; set; }

        public string ProtocolSupport { get; set; }

        public IEnumerable<ICertificateContext> Keys { get; set; }
    }
}
