using System;
using System.Collections.Generic;
using System.ServiceModel;
using Kernel.Cryptography.CertificateManagement;
using Kernel.Cryptography.Signing.Xml;
using Kernel.Federation.MetaData;
using Microsoft.IdentityModel.Protocols.WSFederation.Metadata;
using WsFederationMetadataProvider.Metadata.DescriptorBuilders;

namespace WsFederationMetadataProvider.Metadata
{
    public class IdpSSOMetadataProvider : MetadataGeneratorBase<IdentityProviderSingleSignOnDescriptor>
    {
        public IdpSSOMetadataProvider(IFederationMetadataWriter metadataWriter, ICertificateManager certificateManager, IXmlSignatureManager xmlSignatureManager)
            : base(metadataWriter, certificateManager, xmlSignatureManager)
        {
           
        }

        protected override IEnumerable<RoleDescriptor> GetDescriptors(IMetadataConfiguration configuration)
        {
            var idDescRole = DescriptorBuildersHelper.ResolveAndBuild(typeof(IdentityProviderSingleSignOnDescriptor), configuration);
            var appDescriptor = this.GetApplicationDescriptor(configuration);
            var idpDescriptor = this.GetIdPDescriptor(configuration);
            var tokenDescriptor = this.GetTokenServiceDescriptor(configuration);
            return new[] { appDescriptor, idpDescriptor, tokenDescriptor };
        }

        private SecurityTokenServiceDescriptor GetTokenServiceDescriptor(IMetadataConfiguration configuration)
        {
            var tokenService = new SecurityTokenServiceDescriptor();
            tokenService.ServiceDescription = "http://localhost:8080/idp/status";
            //tokenService.Keys.Add(GetSigningKeyDescriptor());

            //tokenService.PassiveRequestorEndpoints.Add(new EndpointReference(passiveRequestorEndpoint));

            //tokenService.TokenTypesOffered.Add(new Uri(TokenTypes.OasisWssSaml11TokenProfile11));
            //tokenService.TokenTypesOffered.Add(new Uri(TokenTypes.OasisWssSaml2TokenProfile11));

            //ClaimsRepository.GetSupportedClaimTypes().ToList().ForEach(claimType => tokenService.ClaimTypesOffered.Add(new DisplayClaim(claimType)));
            tokenService.ProtocolsSupported.Add(new Uri("http://docs.oasis-open.org/wsfed/federation/200706"));

            //if (ConfigurationRepository.WSTrust.Enabled && ConfigurationRepository.WSTrust.EnableMessageSecurity)
            //{
            //    var addressMessageUserName = new EndpointAddress(_endpoints.WSTrustMessageUserName, null, null, CreateMetadataReader(_endpoints.WSTrustMex), null);
            //    tokenService.SecurityTokenServiceEndpoints.Add(new EndpointReference(addressMessageUserName.Uri.AbsoluteUri));

            //    if (ConfigurationRepository.WSTrust.EnableClientCertificateAuthentication)
            //    {
            //        var addressMessageCertificate = new EndpointAddress(_endpoints.WSTrustMessageCertificate, null, null, CreateMetadataReader(_endpoints.WSTrustMex), null);
            //        tokenService.SecurityTokenServiceEndpoints.Add(new EndpointReference(addressMessageCertificate.Uri.AbsoluteUri));
            //    }
            //}
            //if (ConfigurationRepository.WSTrust.Enabled && ConfigurationRepository.WSTrust.EnableMixedModeSecurity)
            //{
            //    var addressMixedUserName = new EndpointAddress(_endpoints.WSTrustMixedUserName, null, null, CreateMetadataReader(_endpoints.WSTrustMex), null);
                tokenService.SecurityTokenServiceEndpoints.Add(new EndpointAddress("http://localhost:8080/idp/status"));

            //    if (ConfigurationRepository.WSTrust.EnableClientCertificateAuthentication)
            //    {
            //        var addressMixedCertificate = new EndpointAddress(_endpoints.WSTrustMixedCertificate, null, null, CreateMetadataReader(_endpoints.WSTrustMex), null);
            //        tokenService.SecurityTokenServiceEndpoints.Add(new EndpointReference(addressMixedCertificate.Uri.AbsoluteUri));
            //    }
            //}

            //if (tokenService.SecurityTokenServiceEndpoints.Count == 0)
            //    tokenService.SecurityTokenServiceEndpoints.Add(new EndpointReference(_endpoints.WSFederation.AbsoluteUri));

            return tokenService;
        }

        private RoleDescriptor GetApplicationDescriptor(IMetadataConfiguration configuration)
        {
            var idpConfiguration = configuration as IIdpSSOMetadataConfiguration;

            if (idpConfiguration == null)
                throw new InvalidCastException(string.Format("Expected type: {0} but was: {1}", typeof(IdpSSOMetadataConfiguration).Name, configuration.GetType().Name));

            var appDescriptor = new ApplicationServiceDescriptor();

            appDescriptor.ServiceDescription = "http://localhost:8080/idp/status";
            //appDescriptor.Keys.Add(GetSigningKeyDescriptor());

            //appDescriptor.PassiveRequestorEndpoints.Add(new EndpointReference("http://docs.oasis-open.org/wsfed/federation/200706"));
            //appDescriptor.TokenTypesOffered.Add(new Uri(TokenTypes.OasisWssSaml11TokenProfile11));
            //appDescriptor.TokenTypesOffered.Add(new Uri(TokenTypes.OasisWssSaml2TokenProfile11));

            //ClaimsRepository.GetSupportedClaimTypes().ToList().ForEach(claimType => appDescriptor.ClaimTypesOffered.Add(new DisplayClaim(claimType)));
            appDescriptor.ProtocolsSupported.Add(new Uri("http://docs.oasis-open.org/wsfed/federation/200706"));

            return appDescriptor;

            //descriptor.ProtocolsSupported.Add(new Uri("http://docs.oasis-open.org/wsfed/federation/200706"));

            //foreach (var sso in idpConfiguration.SingleSignOnServices)
            //{
            //    var singleSignOnService = new ProtocolEndpoint(new Uri(sso.Binding), new Uri(sso.Location));

            //    descriptor.SingleSignOnServices.Add(singleSignOnService);
            //}

            //return descriptor;
        }
        private RoleDescriptor GetIdPDescriptor(IMetadataConfiguration configuration)
        {
            var idpConfiguration = configuration as IIdpSSOMetadataConfiguration;

            if (idpConfiguration == null)
                throw new InvalidCastException(string.Format("Expected type: {0} but was: {1}", typeof(IdpSSOMetadataConfiguration).Name, configuration.GetType().Name));

            var descriptor = new IdentityProviderSingleSignOnDescriptor();

            descriptor.ProtocolsSupported.Add(new Uri("http://docs.oasis-open.org/wsfed/federation/200706"));

            foreach (var sso in idpConfiguration.SingleSignOnServices)
            {
                var singleSignOnService = new ProtocolEndpoint(new Uri(sso.Binding), new Uri(sso.Location));

                descriptor.SingleSignOnServices.Add(singleSignOnService);
            }

            return descriptor;
        }
    }
}