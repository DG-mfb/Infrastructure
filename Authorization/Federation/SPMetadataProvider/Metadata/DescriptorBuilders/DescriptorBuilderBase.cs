using Kernel.Federation.MetaData;
using Microsoft.IdentityModel.Protocols.WSFederation.Metadata;

namespace WsFederationMetadataProvider.Metadata.DescriptorBuilders
{
    internal abstract class DescriptorBuilderBase<TRole> : IDescriptorBuilder<TRole> where TRole : RoleDescriptor
    {
        public TRole BuildDescriptor(IMetadataConfiguration configuration)
        {
            return this.BuildDescriptorInternal(configuration);
        }

        protected abstract TRole BuildDescriptorInternal(IMetadataConfiguration configuration);
    }
}