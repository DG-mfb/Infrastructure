using System;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;

namespace SecurityManagement.Tests
{
    [TestFixture]
    [Ignore("No tests yet")]
    public class CertificateValidatorTests
    {
        [Test]
        public void RemoteCertificateValidationCallbackTest()
        {
            //ARRANGE
            var validator = new CertificateValidator();
            //ACT

            //ASSERT
            Assert.Throws<NotImplementedException>(() => validator.Validate((X509Certificate2)null));
        }

        [Test]
        public void MetadataSerialisationCertificateTest()
        {
            //ARRANGE
            var validator = new CertificateValidator();
            //ACT

            //ASSERT
            Assert.Throws<NotImplementedException>(() => validator.Validate(null, null, null, System.Net.Security.SslPolicyErrors.None));
        }
    }
}