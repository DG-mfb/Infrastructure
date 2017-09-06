using System;
using System.IdentityModel.Metadata;
using Kernel.Federation.MetaData;

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

            foreach(var protocol in configuration.SupportedProtocols)
            {
                descriptor.ProtocolsSupported.Add(new Uri(protocol));
            }

            foreach (var cs in spConfiguration.ConsumerServices)
            {
                var consumerService = new IndexedProtocolEndpoint(cs.Index, new Uri(cs.Binding), new Uri(cs.Location));

                descriptor.AssertionConsumerServices.Add(cs.Index, consumerService);
            }

            return descriptor;
        }
    }
}