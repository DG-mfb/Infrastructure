using System;
using System.IdentityModel.Metadata;
using System.Xml;
using Federation.Protocols.Request;
using Kernel.Extensions;
using Kernel.Federation.MetaData;
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
            throw new NotImplementedException();
            ////ARRANGE
            //var certificateValidator = new CertificateValidator();
            //var result = String.Empty;
            ////var metadataWriter = new TestMetadatWriter(el => result = el.OuterXml);
            //var metadataWriter = new TestMetadatWriter(el =>
            //{
            //    using (var writer = XmlWriter.Create(@"d:\SPMetadataTest.xml"))
            //    {
            //        el.WriteTo(writer);
            //        writer.Flush();
            //    }

            //});

            //var configuration = new SPSSOMetadataConfiguration
            //{
            //    AuthnRequestsSigned = true,
            //    DescriptorId = "Idp1",
            //    EntityId = new Uri("http://localhost:60879/sp/metadata"),
            //    MetadatFilePathDestination = @"D:\SPSSOMetadata.xml",
            //    SupportedProtocols = new[] { "urn:oasis:names:tc:SAML:2.0:protocol" },
            //    SignMetadata = true,
            //    ConsumerServices = new ConsumerServiceContext[]{new ConsumerServiceContext
            //    {
            //        Index = 0,
            //        IsDefault = true,
            //        Binding = "urn:oasis:names:tc:SAML:2.0:bindings:HTTP-POST",
            //        Location = "http://localhost:60879/api/Account/SSOLogon"
            //    }},
            //    Keys = new CertificateContext[] { new CertificateContext
            //    {
            //        SertificateFilePath = @"D:\Dan\Software\Apira\Certificates\TestCertificates\ApiraTestCert.pfx",
            //        CertificatePassword = StringExtensions.ToSecureString("Password1"),
            //        //SertificateFilePath = @"D:\Dan\Software\SGWI\ThirdParty\devCertsPackage\employeeportaldev.safeguardworld.com.pfx",
            //        //CertificatePassword = StringExtensions.ToSecureString("$Password1!"),
            //        Usage = "Signing",
            //        DefaultForMetadataSigning = true
            //    }}
            //};

            //configuration.Descriptors = new DescriptorContext[]
            //{
            //    new DescriptorContext(typeof(ServiceProviderSingleSignOnDescriptor))
            //};

            //var ssoCryptoProvider = new CertificateManager();
            //var xmlSignatureManager = new XmlSignatureManager();
            //var metadataSerialiser = new FederationMetadataSerialiser(certificateValidator);
            //var sPSSOMetadataProvider = new SPSSOMetadataProvider(metadataWriter, ssoCryptoProvider, xmlSignatureManager, metadataSerialiser, g => configuration);
            
            ////ACT
            //sPSSOMetadataProvider.CreateMetadata();
            ////ASSERT
            //Assert.IsFalse(String.IsNullOrWhiteSpace(result));
        }

        
        [Test]
        public void RedirectUriBuildTest()
        {
            //ARRANGE
            var certManager = new CertificateManager();
            var requestContext = new AuthnRequestContext(null, new Uri("https://dg-mfb/idp/profile/SAML2/Redirect/SSO"));
            var requestBuilder = new AuthnRequestBuilder(certManager);
            //ACT
            var uri = requestBuilder.BuildRedirectUri(requestContext);
            var query = uri.Query;
            //ASSERT
        }
    }
}