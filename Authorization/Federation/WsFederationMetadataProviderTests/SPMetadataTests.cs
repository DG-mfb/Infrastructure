using System;
using System.Collections.Generic;
using System.IdentityModel.Metadata;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml;
using Federation.Protocols.Request;
using Kernel.Extensions;
using Kernel.Federation.MetaData;
using NUnit.Framework;
using SecurityManagement;
using Serialisation.Xml;
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
            //ARRANGE
            var result = String.Empty;
            var metadataWriter = new TestMetadatWriter(el => result = el.OuterXml);
            //var metadataWriter = new TestMetadatWriter(el =>
            //{
            //    using (var writer = XmlWriter.Create(@"d:\test.xml"))
            //    {
            //        el.WriteTo(writer);
            //        writer.Flush();
            //    }

            //});

            var configuration = new SPSSOMetadataConfiguration
            {
                AuthnRequestsSigned = true,
                DescriptorId = "Idp1",
                EntityId = new Uri("http://localhost:64247/sso/saml2/post/AssertionConsumerService.aspx"),
                MetadatFilePathDestination = @"D:\SPSSOMetadata.xml",
                SupportedProtocols = new[] { "urn:oasis:names:tc:SAML:2.0:protocol" },
                SignMetadata = true,
                ConsumerServices = new ConsumerServiceContext[]{new ConsumerServiceContext
                {
                    Index = 0,
                    IsDefault = true,
                    Binding = "urn:oasis:names:tc:SAML:2.0:bindings:HTTP-POST",
                    Location = "http://localhost:64247/sso/saml2/post/AssertionConsumerService.aspx"
                }},
                Keys = new CertificateContext[] { new CertificateContext
                {
                    SertificateFilePath = @"D:\Dan\Software\SGWI\ThirdParty\devCertsPackage\employeeportaldev.safeguardworld.com.pfx",
                    CertificatePassword = StringExtensions.ToSecureString("$Password1!"),
                    Usage = "Signing",
                    DefaultForMetadataSigning = true
                }}
            };

            configuration.Descriptors = new DescriptorContext[]
            {
                new DescriptorContext(typeof(ServiceProviderSingleSignOnDescriptor))
            };

            var ssoCryptoProvider = new CertificateManager();
            var xmlSignatureManager = new XmlSignatureManager();
            var metadataSerialiser = new FederationMetadataSerialiser();
            var sPSSOMetadataProvider = new SPSSOMetadataProvider(metadataWriter, ssoCryptoProvider, xmlSignatureManager, metadataSerialiser, g => configuration);
            
            //ACT
            sPSSOMetadataProvider.CreateMetadata();
            //ASSERT
            Assert.IsFalse(String.IsNullOrWhiteSpace(result));
        }

        [Test]
        public void IdMetadataProviderTest()
        {
            var result = String.Empty;
            var metadataWriter = new TestMetadatWriter(el => result = el.OuterXml);
            //var metadataWriter = new TestMetadatWriter(el =>
            //{
            //    using (var writer = XmlWriter.Create(@"d:\test.xml"))
            //    {
            //        el.WriteTo(writer);
            //        writer.Flush();
            //    }

            //});

            var ssoCryptoProvider = new CertificateManager();
            var xmlSignatureManager = new XmlSignatureManager();
            var metadataSerialiser = new FederationMetadataSerialiser();

            var configuration = new IdpSSOMetadataConfiguration
            {
                WantAuthnRequestsSigned = true,
                DescriptorId = "Idp1",
                EntityId = new Uri("http://localhost:63337/sso/Login.aspx"),
                MetadatFilePathDestination = @"D:\SPSSOMetadata.xml",
                SupportedProtocols = new[] { "urn:oasis:names:tc:SAML:2.0:protocol" },
                SignMetadata = true,
                SingleSignOnServices = new SingleSignOnServiceContext[]{new SingleSignOnServiceContext
                {
                    Binding = "urn:oasis:names:tc:SAML:2.0:bindings:HTTP-POST",
                    Location = "http://localhost:63337/sso/Login.aspx"
                }},
                Keys = new CertificateContext[] { new CertificateContext
                {
                    SertificateFilePath = @"D:\Dan\Software\SGWI\ThirdParty\devCertsPackage\employeeportaldev.safeguardworld.com.pfx",
                    CertificatePassword = "$Password1!".ToSecureString(),
                    Usage = "Signing",
                    DefaultForMetadataSigning = true
                }}
            };

            configuration.Descriptors = new DescriptorContext[]
            {
                new DescriptorContext(typeof(IdentityProviderSingleSignOnDescriptor)),
                new DescriptorContext(typeof(ApplicationServiceDescriptor)),
                new DescriptorContext(typeof(SecurityTokenServiceDescriptor)),

            };

            var idpSOMetadataProvider = new IdpSSOMetadataProvider(metadataWriter, ssoCryptoProvider, xmlSignatureManager, metadataSerialiser, _ => configuration);

            
            
            idpSOMetadataProvider.CreateMetadata();
            Assert.IsFalse(String.IsNullOrWhiteSpace(result));
        }


        [Test]
        public void RedirectUriBuildTest()
        {
            //ARRANGE
            var authnRequest = new AuthnRequest { Id = Guid.NewGuid().ToString(), IsPassive = true, Destination = "http://localhost", Version = "2.0" };
            authnRequest.Issuer = new NameId { Value = "http://localhost" };
            var audienceRestrictions = new List<ConditionAbstract>();
            var audienceRestriction = new AudienceRestriction { Audience = new List<string>() { "http://localhost" } };
            audienceRestrictions.Add(audienceRestriction);

            authnRequest.Conditions = new Conditions { Items = audienceRestrictions };

            var serialiser = new XMLSerialiser();
            var ms = new MemoryStream();
            var sb = new StringBuilder();
            serialiser.XmlNamespaces.Add("samlp", Saml20Constants.Protocol);
            serialiser.XmlNamespaces.Add("saml", Saml20Constants.Assertion);
            //ACT
            using (ms)
            {
                serialiser.Serialize(ms, new[] { authnRequest });
                ms.Position = 0;
                var streamReader = new StreamReader(ms);
                var xmlString = streamReader.ReadToEnd();
                ms.Position = 0;
                var encoded = DeflateEncode(xmlString);// Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length, Base64FormattingOptions.None);
                var result = "https://dg-mfb/idp/profile/SAML2/Redirect/SSO?" + "SAMLRequest=" + Uri.EscapeDataString(encoded);
            }
            //ASSERT
        }

        public static string DeflateEncode(string val)
        {
            var memoryStream = new MemoryStream();
            using (var writer = new StreamWriter(new DeflateStream(memoryStream, CompressionMode.Compress, true), new UTF8Encoding(false)))
            {
                writer.Write(val);
                writer.Close();

                return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length, Base64FormattingOptions.None);
            }
        }
    }
}