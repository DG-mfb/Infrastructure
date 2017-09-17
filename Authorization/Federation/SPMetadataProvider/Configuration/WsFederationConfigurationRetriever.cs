using System;
using System.IdentityModel.Metadata;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Federation.Protocols;
using Kernel.Cryptography.Validation;
using Kernel.Federation.Protocols;
using Kernel.Initialisation;
using WsMetadataSerialisation.Serialisation;

namespace WsFederationMetadataProvider.Configuration
{
    //ToDo: refactor all statics. Use DI
    public class WsFederationConfigurationRetriever : IConfigurationRetriever<MetadataBase>
    {
        private static readonly XmlReaderSettings SafeSettings = new XmlReaderSettings()
        {
            DtdProcessing = DtdProcessing.Prohibit
        };

        public static Task<MetadataBase> GetAsync(string address, CancellationToken cancel)
        {
            return WsFederationConfigurationRetriever.GetAsync(address, (IDocumentRetriever)new HttpDocumentRetriever(), cancel);
        }

        public static Task<MetadataBase> GetAsync(string address, HttpClient httpClient, CancellationToken cancel)
        {
            return WsFederationConfigurationRetriever.GetAsync(address, (IDocumentRetriever)new HttpDocumentRetriever(httpClient), cancel);
        }

        Task<MetadataBase> IConfigurationRetriever<MetadataBase>.GetConfigurationAsync(string address, IDocumentRetriever retriever, CancellationToken cancel)
        {
            return WsFederationConfigurationRetriever.GetAsync(address, retriever, cancel);
        }

        public static async Task<MetadataBase> GetAsync(string address, IDocumentRetriever retriever, CancellationToken cancel)
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
            using (XmlReader reader = XmlReader.Create((TextReader)new StringReader(document), WsFederationConfigurationRetriever.SafeSettings))
                federationConfiguration = new FederationMetadataSerialiser(validator).ReadMetadata(reader);
            return federationConfiguration;
        }
    }
}