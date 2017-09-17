using System;
using System.Threading.Tasks;
using Kernel.DependancyResolver;
using Kernel.Federation.MetaData;
using Kernel.Federation.MetaData.Configuration.EntityDescriptors;
using Shared.Initialisation;
using WsFederationMetadataProvider.Metadata;

namespace WsFederationMetadataProvider.Initialisation
{
    public class WsFederationMetadataProviderInitialiser : Initialiser
    {
        public override byte Order
        {
            get { return 1; }
        }

        protected override Task InitialiseInternal(IDependencyResolver dependencyResolver)
        {
            dependencyResolver.RegisterType<SPSSOMetadataProvider>(Lifetime.Transient);
            dependencyResolver.RegisterFactory<Func<MetadataType, EntityDesriptorConfiguration>>(t =>
            {
                return _ =>
                {
                    if (_ == MetadataType.SP)
                        return this.GetSPConfiguration();
                    throw new NotImplementedException();
                };
            }, Lifetime.Singleton);
            return Task.CompletedTask;
        }

        private EntityDesriptorConfiguration GetSPConfiguration()
        {
            throw new NotImplementedException();
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
            //return configuration;
        }
    }
}