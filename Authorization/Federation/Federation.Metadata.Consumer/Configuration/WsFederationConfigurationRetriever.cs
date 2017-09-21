using System;
using System.IdentityModel.Metadata;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Kernel.Cryptography.Validation;
using Kernel.Federation.MetadataConsumer;
using Kernel.Initialisation;
using WsMetadataSerialisation.Serialisation;

namespace Federation.Metadata.Consumer.Configuration
{
    public class WsFederationConfigurationRetriever : IConfigurationRetriever<MetadataBase>
    {
        private readonly IDocumentRetriever _retriever;
        private readonly XmlReaderSettings _safeSettings = new XmlReaderSettings()
        {
            DtdProcessing = DtdProcessing.Prohibit
        };
        public WsFederationConfigurationRetriever(IDocumentRetriever retriever)
        {
            this._retriever = retriever;
        }

        public Task<MetadataBase> GetAsync(string address, CancellationToken cancel)
        {
            return this.GetAsync(address, this._retriever, cancel);
        }

        private async Task<MetadataBase> GetAsync(string address, IDocumentRetriever retriever, CancellationToken cancel)
        {
            var validator = ApplicationConfiguration.Instance.DependencyResolver.Resolve<ICertificateValidator>();
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentNullException("address");
            if (retriever == null)
                throw new ArgumentNullException("retriever");
            string str = await retriever.GetDocumentAsync(address, cancel);
            string document = str;
            str = (string)null;
            MetadataBase federationConfiguration;
            using (XmlReader reader = XmlReader.Create((TextReader)new StringReader(document), this._safeSettings))
                federationConfiguration = new FederationMetadataSerialiser(validator).ReadMetadata(reader);
            return federationConfiguration;
        }
    }
}