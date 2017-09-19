using System;
using System.Xml;
using InlineMetadataContextProvider;
using Kernel.Federation.MetaData;
using NUnit.Framework;
using SecurityManagement;
using WsFederationMetadataProvider.Metadata;
using WsFederationMetadataProviderTests.Mock;
using WsMetadataSerialisation.Serialisation;

namespace WsFederationMetadataProviderTests
{
    [TestFixture]
    public class SPMetadataTests
    {
        [Test]
        public void SPMetadataProviderTest()
        {
            ////ARRANGE
           
            var result = String.Empty;
            var metadataWriter = new TestMetadatWriter(el => result = el.OuterXml);
            //var metadataWriter = new TestMetadatWriter(el =>
            //{
            //    using (var writer = XmlWriter.Create(@"D:\Dan\Software\Apira\SPMetadata\SPMetadata.xml"))
            //    {
            //        el.WriteTo(writer);
            //        writer.Flush();
            //    }

            //});


                var contextBuilder = new InlineMetadataContextBuilder();
            var context = contextBuilder.BuildContext();

            var certificateValidator = new CertificateValidator();
            var ssoCryptoProvider = new CertificateManager();
            
            var metadataSerialiser = new FederationMetadataSerialiser(certificateValidator);
           
            var sPSSOMetadataProvider = new SPSSOMetadataProvider(metadataWriter, ssoCryptoProvider, metadataSerialiser, g => context);
            
            //ACT
            sPSSOMetadataProvider.CreateMetadata(MetadataType.SP);
            //ASSERT
            Assert.IsFalse(String.IsNullOrWhiteSpace(result));
        }
    }
}