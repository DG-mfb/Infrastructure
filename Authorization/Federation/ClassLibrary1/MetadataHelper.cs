﻿using System;
using System.IdentityModel.Metadata;
using System.Security.Cryptography.X509Certificates;
using Kernel.Cryptography.CertificateManagement;
using Kernel.Federation.MetaData.Configuration;
using Kernel.Federation.MetaData.Configuration.Cryptography;
using Kernel.Federation.MetaData.Configuration.EndPoint;
using Kernel.Federation.MetaData.Configuration.EntityDescriptors;
using Kernel.Federation.MetaData.Configuration.Organisation;
using Kernel.Federation.MetaData.Configuration.RoleDescriptors;

namespace InlineMetadataContextProvider
{
    internal class MetadataHelper
    {
        public static EntityDesriptorConfiguration BuildEntityDesriptorConfiguration()
        {
            var federationId = String.Format("{0}_{1}", "flowz", Guid.NewGuid());
            var entityDescriptorConfiguration = new EntityDesriptorConfiguration
            {
                CacheDuration = TimeSpan.FromDays(100),
                EntityId = "Imperial.flowz.co.uk",
                Id = federationId,
                ValidUntil = new DateTimeOffset(DateTime.Now.AddDays(30)),
                Organisation = MetadataHelper.BuikdOrganisationConfiguration()
            };
            return entityDescriptorConfiguration;
        }

        public static OrganisationConfiguration BuikdOrganisationConfiguration()
        {
            var orgConfiguration = new OrganisationConfiguration
            {
                OrganisationContacts = new ContactConfiguration()
            };
            orgConfiguration.Names.Add(new LocalizedConfigurationEntry
            {
                Name = "Apira LTD",
                DisplayName = "Flowz",
            });
            orgConfiguration.Urls.Add(new Uri("https://apira.co.uk/"));

            var contact = new Kernel.Federation.MetaData.Configuration.Organisation.ContactPerson
            {
                ContactType = Kernel.Federation.MetaData.Configuration.Organisation.ContactType.Technical,
                ForeName = "John",
                SurName = "Murphy",

            };
            contact.Emails.Add("john.murphy@flowz.co.uk");
            
            orgConfiguration.OrganisationContacts.PersonContact.Add(contact);
            return orgConfiguration;
        }
        public static KeyDescriptorConfiguration BuildKeyDescriptorConfiguration()
        {
            var certificateContext = new X509CertificateContext
            {
                StoreName = "TestCertStore",
                SearchCriteria = "ApiraTestCertificate",
                ValidOnly = false,
                SearchCriteriaType = X509FindType.FindBySubjectName,
                StoreLocation = StoreLocation.LocalMachine
            };
            
            var keyDescriptorConfiguration = new KeyDescriptorConfiguration
            {
                IsDefault = true,
                Use = KeyUsage.Signing,
                CertificateContext = certificateContext
            };
            return keyDescriptorConfiguration;
        }

        public static SPSSODescriptorConfiguration BuildSPSSODescriptorConfiguration()
        {
            var sPSSODescriptorConfiguration = new SPSSODescriptorConfiguration
            {
                WantAssertionsSigned = true,
                ValidUntil = new DateTimeOffset(DateTime.Now.AddDays(100)),
                Organisation = MetadataHelper.BuikdOrganisationConfiguration(),
                AuthenticationRequestsSigned = true,
                CacheDuration = TimeSpan.FromDays(100),
                RoleDescriptorType = typeof(ServiceProviderSingleSignOnDescriptor)
            };
            //supported protocols
            sPSSODescriptorConfiguration.ProtocolSupported.Add(new Uri("urn:oasis:names:tc:SAML:2.0:protocol"));
            //key descriptors
            var keyDescriptorConfiguration = MetadataHelper.BuildKeyDescriptorConfiguration();
            sPSSODescriptorConfiguration.KeyDescriptors.Add(keyDescriptorConfiguration);

            //assertinon service
            var indexedEndPointConfiguration = new IndexedEndPointConfiguration
            {
                Index = 0,
                IsDefault = true,
                Binding = new Uri("urn:oasis:names:tc:SAML:2.0:bindings:HTTP-POST"),
                Location = new Uri("http://localhost:60879/api/Account/SSOLogon")
            };
            sPSSODescriptorConfiguration.AssertionConsumerServices.Add(indexedEndPointConfiguration);

            return sPSSODescriptorConfiguration;
        }
    }
}