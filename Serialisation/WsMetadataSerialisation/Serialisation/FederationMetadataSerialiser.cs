using System.IdentityModel.Metadata;
using System.IdentityModel.Selectors;
using System.IO;
using System.Xml;
using Kernel.Cryptography.Validation;
using Kernel.Federation.MetaData;

namespace WsMetadataSerialisation.Serialisation
{
    public class FederationMetadataSerialiser : MetadataSerializer, IMetadataSerialiser<MetadataBase>
    {
        //ToDo: resolve validator from conficuration.
        //ICertificateValidator _certificateValidator;
        //Needs certificate configuration
        //inject base class or introduce other interface
        public FederationMetadataSerialiser(ICertificateValidator certificateValidator)
        {
            base.CertificateValidator = (X509CertificateValidator)certificateValidator;
            base.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.Custom;
        }
        public void Serialise(XmlWriter writer, MetadataBase metadata)
        {
            base.WriteMetadata(writer, metadata);
        }

        public MetadataBase Deserialise(Stream stream)
        {
            return base.ReadMetadata(stream);
        }

        public MetadataBase Deserialise(XmlReader xmlReader)
        {
            return base.ReadMetadata(xmlReader);
        }

        protected override bool ReadCustomElement<T>(XmlReader reader, T target)
        {
            return base.ReadCustomElement(reader, target);
        }
    }
}