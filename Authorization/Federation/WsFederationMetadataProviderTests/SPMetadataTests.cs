﻿using System;
using System.IdentityModel.Metadata;
using System.Xml;
using Kernel.Extensions;
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

            var ssoCryptoProvider = new CertificateManager();
            var xmlSignatureManager = new XmlSignatureManager();
            var metadataSerialiser = new FederationMetadataSerialiser();
            var sPSSOMetadataProvider = new SPSSOMetadataProvider(metadataWriter, ssoCryptoProvider, xmlSignatureManager, metadataSerialiser, g => configuration);

            

            configuration.Descriptors = new DescriptorContext[]
            {
                new DescriptorContext(typeof(ServiceProviderSingleSignOnDescriptor))
            };

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
    }
}