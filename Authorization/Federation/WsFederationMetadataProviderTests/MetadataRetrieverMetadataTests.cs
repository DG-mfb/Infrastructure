using System;
using System.IdentityModel.Metadata;
using System.Net.Http;
using System.Net.Security;
using System.Threading;
using System.Threading.Tasks;
using Federation.Protocols;
using NUnit.Framework;
using WsFederationMetadataProvider.Configuration;

namespace WsFederationMetadataProviderTests
{
    [TestFixture]
    public class MetadataRetrieverMetadataTests
    {
        [Test]
        public async Task HttpDocumentRetrieverTest()
        {
            //ARRANGE
            var webRequestHandler = new WebRequestHandler();
            webRequestHandler.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback((_, __, ___, ____) => true);
            var httpClient = new HttpClient(webRequestHandler);
            var documentRetrieer = new HttpDocumentRetriever(httpClient);

            //ACT
            var document = await documentRetrieer.GetDocumentAsync("https://dg-mfb/idp/shibboleth", new CancellationToken());
            //ASSERT
            Assert.IsFalse(String.IsNullOrWhiteSpace(document));
        }

        [Test]
        public async Task WsFederationConfigurationRetrieverTest()
        {
            //ARRANGE
            var webRequestHandler = new WebRequestHandler();
            webRequestHandler.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback((_, __, ___, ____) => true);
            var httpClient = new HttpClient(webRequestHandler);
            var documentRetrieer = new HttpDocumentRetriever(httpClient);

            //ACT
            var baseMetadata = await WsFederationConfigurationRetriever.GetAsync("https://dg-mfb/idp/shibboleth", documentRetrieer, new CancellationToken());
            var metadata = baseMetadata as EntityDescriptor;
            //ASSERT
            Assert.IsTrue(metadata != null);
            Assert.AreEqual(1, metadata.RoleDescriptors.Count);
        }
    }
}