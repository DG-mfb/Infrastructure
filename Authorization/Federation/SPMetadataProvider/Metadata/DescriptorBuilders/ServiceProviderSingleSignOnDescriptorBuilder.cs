using System;
using System.IdentityModel.Metadata;
using Kernel.Federation.MetaData.Configuration.RoleDescriptors;

namespace WsFederationMetadataProvider.Metadata.DescriptorBuilders
{
    internal class ServiceProviderSingleSignOnDescriptorBuilder : DescriptorBuilderBase<ServiceProviderSingleSignOnDescriptor>
    {
        protected override ServiceProviderSingleSignOnDescriptor BuildDescriptorInternal(RoleDescriptorConfiguration configuration)
        {
            var spConfiguration = configuration as SPSSODescriptorConfiguration;

            if (spConfiguration == null)
                throw new InvalidCastException(string.Format("Expected type: {0} but was: {1}", typeof(SPSSOMetadataConfiguration).Name, configuration.GetType().Name));

            var descriptor = new ServiceProviderSingleSignOnDescriptor();

            foreach(var protocol in configuration.ProtocolSupported)
            {
                descriptor.ProtocolsSupported.Add(protocol);
            }

            foreach (var cs in spConfiguration.AssertionConsumerServices)
            {
                var consumerService = new IndexedProtocolEndpoint(cs.Index, cs.Binding, cs.Location);

                descriptor.AssertionConsumerServices.Add(cs.Index, consumerService);
            }

            return descriptor;
        }
    }
}