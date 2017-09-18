using System;
using Federation.Protocols.Request;
using Kernel.Federation.MetaData;
using Kernel.Federation.MetaData.Configuration;
using Kernel.Federation.Protocols;
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
            //    using (var writer = XmlWriter.Create(@"d:\SPMetadataTest.xml"))
            //    {
            //        el.WriteTo(writer);
            //        writer.Flush();
            //    }

            //});
            
            var entityDescriptorConfiguration = MetadataHelper.BuildEntityDesriptorConfiguration();
            
            var keyDescriptorConfiguration = MetadataHelper.BuildKeyDescriptorConfiguration();
            entityDescriptorConfiguration.KeyDescriptors.Add(keyDescriptorConfiguration);

            var spDescriptorConfigurtion = MetadataHelper.BuildSPSSODescriptorConfiguration();
            entityDescriptorConfiguration.RoleDescriptors.Add(spDescriptorConfigurtion);
            
            var certificateValidator = new CertificateValidator();
            var ssoCryptoProvider = new CertificateManager();
            var xmlSignatureManager = new XmlSignatureManager();
            var metadataSerialiser = new FederationMetadataSerialiser(certificateValidator);
            var context = new MetadataContext
            {
                EntityDesriptorConfiguration = entityDescriptorConfiguration,
                SignMetadata = true
            };
            context.KeyDescriptors.Add(keyDescriptorConfiguration);
            var sPSSOMetadataProvider = new SPSSOMetadataProvider(metadataWriter, ssoCryptoProvider, xmlSignatureManager, metadataSerialiser, g => context);
            
            //ACT
            sPSSOMetadataProvider.CreateMetadata(MetadataType.SP);
            //ASSERT
            Assert.IsFalse(String.IsNullOrWhiteSpace(result));
        }
    }
}