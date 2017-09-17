using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Kernel.Federation.MetaData.Configuration.Cryptography;
using NUnit.Framework;
using WsFederationMetadataProvider.Metadata.DescriptorBuilders;
using WsFederationMetadataProviderTests.Mock;

namespace WsFederationMetadataProviderTests
{
    [TestFixture]
    internal class CertificateStoreTests
    {
        [Test]
        public void X509CertStoreConfigurationTestTest()
        {
            //ARRANGE
            var certConfiguration = new X509StoreCertificateConfiguration("TestCertStore", "ApiraTestCertificate");
            //ACT
            X509Certificate2 cert = null;
            //ASSERT
            Assert.DoesNotThrow(() => cert = certConfiguration.GetX509Certificate2());
            Assert.NotNull(cert);
        }
    }
}