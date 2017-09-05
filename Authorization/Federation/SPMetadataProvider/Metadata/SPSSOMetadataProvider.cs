using Kernel.Cryptography.CertificateManagement;
using Kernel.Cryptography.Signing.Xml;
using Kernel.Federation.MetaData;

namespace WsFederationMetadataProvider.Metadata
{
    public class SPSSOMetadataProvider : MetadataGeneratorBase
    {
        public SPSSOMetadataProvider(IFederationMetadataWriter metadataWriter, ICertificateManager certificateManager, IXmlSignatureManager xmlSignatureManager)
            :base(metadataWriter, certificateManager, xmlSignatureManager)
        { }

        //protected override IEnumerable<RoleDescriptor> GetDescriptors(IMetadataConfiguration configuration)
        //{
           
        //}
    }
}