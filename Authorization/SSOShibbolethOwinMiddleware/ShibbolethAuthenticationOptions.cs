using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Net.Http;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace SSOShibbolethOwinMiddleware
{
    public class ShibbolethAuthenticationOptions : AuthenticationOptions
    {
        public ICertificateValidator BackchannelCertificateValidator { get; set; }

        private ICollection<ISecurityTokenValidator> _securityTokenHandlers = new List<ISecurityTokenValidator>()
        {
            (ISecurityTokenValidator)new Microsoft.IdentityModel.Tokens.Saml2SecurityTokenHandler(),
            //(Microsoft.IdentityModel.Tokens.ISecurityTokenValidator) new Microsoft.IdentityModel.Tokens.SamlSecurityTokenHandler(),
            //(Microsoft.IdentityModel.Tokens.ISecurityTokenValidator) new JwtSecurityTokenHandler()
        };
        private Microsoft.IdentityModel.Tokens.TokenValidationParameters _tokenValidationParameters;


        public HttpMessageHandler BackchannelHttpHandler { get; set; }

        public TimeSpan BackchannelTimeout { get; set; }

        public string Caption
        {
            get
            {
                return this.Description.Caption;
            }
            set
            {
                this.Description.Caption = value;
            }
        }

        public object Configuration { get; set; }

        public string MetadataAddress { get; set; }

        public IConfigurationManager<object> ConfigurationManager { get; set; }

        public bool RefreshOnIssuerKeyNotFound { get; set; }

        //public WsFederationAuthenticationNotifications Notifications { get; set; }

        public ICollection<ISecurityTokenValidator> SecurityTokenHandlers
        {
            get
            {
                return this._securityTokenHandlers;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("SecurityTokenHandlers");
                this._securityTokenHandlers = value;
            }
        }

        public string SignInAsAuthenticationType
        {
            get
            {
                return this.TokenValidationParameters.AuthenticationType;
            }
            set
            {
                this.TokenValidationParameters.AuthenticationType = value;
            }
        }

        public ISecureDataFormat<AuthenticationProperties> StateDataFormat { get; set; }

        public Microsoft.IdentityModel.Tokens.TokenValidationParameters TokenValidationParameters
        {
            get
            {
                return this._tokenValidationParameters;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("TokenValidationParameters");
                this._tokenValidationParameters = value;
            }
        }

        public string Wreply { get; set; }

        public string SignOutWreply { get; set; }

        public string Wtrealm { get; set; }

        public PathString CallbackPath { get; set; }

        public bool UseTokenLifetime { get; set; }

        public ShibbolethAuthenticationOptions()
      : base("Shibboleth")
    {
            this.Caption = "Shibboleth";
            this.AuthenticationMode = AuthenticationMode.Active;
            this._tokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters();
            this.BackchannelTimeout = TimeSpan.FromMinutes(1.0);
            this.UseTokenLifetime = true;
            this.RefreshOnIssuerKeyNotFound = true;
        }
    }
}