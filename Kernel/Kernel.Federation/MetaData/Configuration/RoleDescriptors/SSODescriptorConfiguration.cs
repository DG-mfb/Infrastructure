using System;
using System.Collections.Generic;
using Kernel.Federation.MetaData.Configuration.EndPoint;

namespace Kernel.Federation.MetaData.Configuration.RoleDescriptors
{
    public class SSODescriptorConfiguration : RoleDescriptorConfiguration
    {
        public ICollection<Uri> NameIdentifierFormats { get; }
        public IndexedEndPointConfiguration ArtifactResolutionServices { get; }
        public ICollection<EndPointConfiguration> SingleLogoutServices { get; }
    }
}