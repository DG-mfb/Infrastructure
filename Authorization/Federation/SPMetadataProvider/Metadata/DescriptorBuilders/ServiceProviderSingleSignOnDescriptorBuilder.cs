using System;
using Kernel.Federation.MetaData;
using Microsoft.IdentityModel.Protocols.WSFederation.Metadata;

namespace WsFederationMetadataProvider.Metadata.DescriptorBuilders
{
    internal class ServiceProviderSingleSignOnDescriptorBuilder : DescriptorBuilderBase<ServiceProviderSingleSignOnDescriptor>
    {
        protected override ServiceProviderSingleSignOnDescriptor BuildDescriptorInternal(IMetadataConfiguration configuration)
        {
            var spConfiguration = configuration as ISPSSOMetadataConfiguration;

            if (spConfiguration == null)
                throw new InvalidCastException(string.Format("Expected type: {0} but was: {1}", typeof(SPSSOMetadataConfiguration).Name, configuration.GetType().Name));

            var descriptor = new ServiceProviderSingleSignOnDescriptor();

            descriptor.ProtocolsSupported.Add(new Uri("http://docs.oasis-open.org/wsfed/federation/200706"));

            foreach (var cs in spConfiguration.ConsumerServices)
            {
                var consumerService = new IndexedProtocolEndpoint(cs.Index, new Uri(cs.Binding), new Uri(cs.Location));

                descriptor.AssertionConsumerService.Add(cs.Index, consumerService);
            }

            return descriptor;
        }
    }
}