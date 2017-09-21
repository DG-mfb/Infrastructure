using System;
using System.IdentityModel.Metadata;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Kernel.Federation.MetaData;
using Kernel.Federation.MetadataConsumer;

namespace Federation.Metadata.Consumer.Configuration
{
    public class WsFederationConfigurationRetriever : IConfigurationRetriever<MetadataBase>
    {
        private readonly IDocumentRetriever _retriever;
        private readonly IMetadataSerialiser<MetadataBase> _metadataSerialiser;

        private readonly XmlReaderSettings _safeSettings = new XmlReaderSettings()
        {
            DtdProcessing = DtdProcessing.Prohibit
        };
        public WsFederationConfigurationRetriever(IDocumentRetriever retriever, IMetadataSerialiser<MetadataBase> metadataSerialiser)
        {
            this._metadataSerialiser = metadataSerialiser;
            this._retriever = retriever;
        }

        public Task<MetadataBase> GetAsync(string address, CancellationToken cancel)
        {
            return this.GetAsync(address, this._retriever, cancel);
        }

        private async Task<MetadataBase> GetAsync(string address, IDocumentRetriever retriever, CancellationToken cancel)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentNullException("address");
            if (retriever == null)
                throw new ArgumentNullException("retriever");
            string str = await retriever.GetDocumentAsync(address, cancel);
            string document = str;
            str = (string)null;
            MetadataBase federationConfiguration;
            using (XmlReader reader = XmlReader.Create((TextReader)new StringReader(document), this._safeSettings))
            {
                federationConfiguration =this._metadataSerialiser.Deserialise(reader);
                return federationConfiguration;
            }
        }
    }
}