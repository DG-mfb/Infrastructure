using System;
using System.IdentityModel.Metadata;
using Kernel.Cryptography.CertificateManagement;
using Kernel.Cryptography.Signing.Xml;
using Kernel.Federation.MetaData;
using Kernel.Federation.MetaData.Configuration.EntityDescriptors;

namespace WsFederationMetadataProvider.Metadata
{
    public class SPSSOMetadataProvider : MetadataGeneratorBase, ISPMetadataGenerator
    {
        public SPSSOMetadataProvider(IFederationMetadataWriter metadataWriter, ICertificateManager certificateManager, IXmlSignatureManager xmlSignatureManager, IMetadataSerialiser<MetadataBase> serialiser, Func<MetadataType, EntityDesriptorConfiguration> configuration)
            :base(metadataWriter, certificateManager, xmlSignatureManager, serialiser, configuration)
        { }
    }
}