using System;
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
        public void X509CertStoreConfigurationTestTest_should_pass_subject_localMachine_invalid_included()
        {
            //ARRANGE
            var certConfiguration = new X509StoreCertificateConfiguration("TestCertStore", "ApiraTestCertificate", X509FindType.FindBySubjectName, StoreLocation.LocalMachine, false);
            //ACT
            X509Certificate2 cert = null;
            //ASSERT
            Assert.DoesNotThrow(() => cert = certConfiguration.GetX509Certificate2());
            Assert.NotNull(cert);
        }

        [Test]
        public void X509CertStoreConfigurationTestTest_should_pass_subject_current_user_invalid_included()
        {
            //ARRANGE
            var certConfiguration = new X509StoreCertificateConfiguration("TestCertStore", "ApiraTestCertificate", X509FindType.FindBySubjectName, StoreLocation.CurrentUser, false);
            //ACT
            X509Certificate2 cert = null;
            //ASSERT
            Assert.DoesNotThrow(() => cert = certConfiguration.GetX509Certificate2());
            Assert.NotNull(cert);
        }

        [Test]
        public void X509CertStoreConfigurationTestTest_should_fail_subject_localMachine_valid_only()
        {
            //ARRANGE
            var certConfiguration = new X509StoreCertificateConfiguration("TestCertStore", "ApiraTestCertificate", X509FindType.FindBySubjectName, StoreLocation.LocalMachine, true);
            //ACT
            X509Certificate2 cert = null;
            //ASSERT
            Assert.Throws<InvalidOperationException>(() => cert = certConfiguration.GetX509Certificate2());
        }
    }
}