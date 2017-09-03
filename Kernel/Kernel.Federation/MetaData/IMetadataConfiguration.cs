using System;
using System.Collections.Generic;

namespace Kernel.Federation.MetaData
{
    public interface IMetadataConfiguration
    {
        string MetadatFilePathDestination { get; set; }

        bool SignMetadata { get; set; }

        Uri EntityId { get; set; }

        string DescriptorId { get; set; }

        string ProtocolSupport { get; set; }

        IEnumerable<ICertificateContext> Keys { get; set; }
    }
}