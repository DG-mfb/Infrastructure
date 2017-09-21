﻿using System;
using System.IdentityModel.Metadata;
using System.Net.Http;
using System.Net.Security;
using System.Threading;
using System.Threading.Tasks;
using Federation.Metadata.Consumer.Configuration;
using Federation.Metadata.HttpRetriever;
using NUnit.Framework;

namespace Federation.Metadata.Consumer.Tests
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
            var documentRetrieer = new HttpDocumentRetriever(() => httpClient);

            //ACT
            
            //var document = await documentRetrieer.GetDocumentAsync("https://dg-mfb/idp/shibboleth", new CancellationToken());
            var document = await documentRetrieer.GetDocumentAsync("https://www.testshib.org/metadata/testshib-providers.xml", new CancellationToken());
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
            var documentRetrieer = new HttpDocumentRetriever(() => httpClient);
            var configurationRetriever = new WsFederationConfigurationRetriever(documentRetrieer);
           
            

            //ACT
            //var baseMetadata = await WsFederationConfigurationRetriever.GetAsync("https://dg-mfb/idp/shibboleth", documentRetrieer, new CancellationToken());
            var baseMetadata = await configurationRetriever.GetAsync("https://www.testshib.org/metadata/testshib-providers.xml", new CancellationToken());
            var metadata = baseMetadata as EntityDescriptor;
            //ASSERT
            Assert.IsTrue(metadata != null);
            Assert.AreEqual(1, metadata.RoleDescriptors.Count);
        }
    }
}