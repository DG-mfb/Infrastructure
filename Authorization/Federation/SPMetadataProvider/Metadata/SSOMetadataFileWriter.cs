using System.Xml;
using Kernel.Federation.MetaData;

namespace WsFederationMetadataProvider.Metadata
{
    public class SSOMetadataFileWriter : IFederationMetadataWriter
    {
        public void Write(XmlElement xml, IMetadataConfiguration configuration)
        {
            using (var w = XmlWriter.Create(configuration.MetadatFilePathDestination))
            {
                xml.WriteTo(w);
            }
        }
    }
}