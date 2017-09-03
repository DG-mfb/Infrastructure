using System;
using Kernel.Cryptography.CertificateManagement;
using Kernel.Cryptography.Signing.Xml;
using Kernel.Federation.MetaData;
using Microsoft.IdentityModel.Protocols.WSFederation.Metadata;

namespace WsFederationMetadataProvider.Metadata
{
    public class IdpSSOMetadataProvider : MetadataGeneratorBase<IdentityProviderSingleSignOnDescriptor>
    {
        public IdpSSOMetadataProvider(IFederationMetadataWriter metadataWriter, ICertificateManager certificateManager, IXmlSignatureManager xmlSignatureManager)
            : base(metadataWriter, certificateManager, xmlSignatureManager)
        {
           
        }

        protected override RoleDescriptor GetDescriptor(IMetadataConfiguration configuration)
        {
            var idpConfiguration = configuration as IIdpSSOMetadataConfiguration;

            if (idpConfiguration == null)
                throw new InvalidCastException(string.Format("Expected type: {0} but was: {1}", typeof(IdpSSOMetadataConfiguration).Name, configuration.GetType().Name));

            var descriptor = new IdentityProviderSingleSignOnDescriptor
            {
                //ID = configuration.DescriptorId,
                //WantAuthnRequestsSigned = idpConfiguration.WantAuthnRequestsSigned,
                //ProtocolSupportEnumeration = idpConfiguration.ProtocolSupport
            };

            
            foreach (var sso in idpConfiguration.SingleSignOnServices)
            {
                var singleSignOnService = new ProtocolEndpoint(new Uri(sso.Binding), new Uri(sso.Location));

                descriptor.SingleSignOnServices.Add(singleSignOnService);
            }

            return descriptor;
        }

        protected override Action<EntityDescriptor, IdentityProviderSingleSignOnDescriptor> AssignmentAction
        {
            get { return (ed, d) => ed.RoleDescriptors.Add(d); }
        }
    }
}