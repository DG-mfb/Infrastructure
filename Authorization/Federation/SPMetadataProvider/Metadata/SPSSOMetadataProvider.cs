using System;
using Kernel.Cryptography.CertificateManagement;
using Kernel.Cryptography.Signing.Xml;
using Kernel.Federation.MetaData;
using Microsoft.IdentityModel.Protocols.WSFederation.Metadata;

namespace SPMetadataProvider.Metadata
{
    public class SPSSOMetadataProvider : MetadataGeneratorBase<ServiceProviderSingleSignOnDescriptor>
    {
        public SPSSOMetadataProvider(IFederationMetadataWriter metadataWriter, ICertificateManager certificateManager, IXmlSignatureManager xmlSignatureManager)
            :base(metadataWriter, certificateManager, xmlSignatureManager)
        { }

        protected override RoleDescriptor GetDescriptor(IMetadataConfiguration configuration)
        {
            var spConfiguration = configuration as ISPSSOMetadataConfiguration;

            if (spConfiguration == null)
                throw new InvalidCastException(string.Format("Expected type: {0} but was: {1}", typeof(SPSSOMetadataConfiguration).Name, configuration.GetType().Name));

            var descriptor = new ServiceProviderSingleSignOnDescriptor
            {
                //ID = configuration.DescriptorId,
                //AuthnRequestsSigned = spConfiguration.AuthnRequestsSigned,
                //ProtocolSupportEnumeration = spConfiguration.ProtocolSupport
            };

            foreach (var cs in spConfiguration.ConcumerServices)
            {
                var consumerService = new IndexedProtocolEndpoint(cs.Index, new Uri(cs.Binding), new Uri(cs.Location));
                
                descriptor.AssertionConsumerService.Add(cs.Index, consumerService);
            }

            return descriptor;
        }

        protected override Action<EntityDescriptor, ServiceProviderSingleSignOnDescriptor> AssignmentAction
        {
            get
            {
                return (ed, d) => ed.RoleDescriptors.Add(d);
            }
        }
    }
}