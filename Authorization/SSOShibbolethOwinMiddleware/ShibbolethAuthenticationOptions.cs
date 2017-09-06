using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Saml2;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace SSOShibbolethOwinMiddleware
{
    public class ShibbolethAuthenticationOptions : AuthenticationOptions
    {
        public ICertificateValidator BackchannelCertificateValidator { get; set; }

        private ICollection<ISecurityTokenValidator> _securityTokenHandlers = (ICollection<ISecurityTokenValidator>)new Collection<ISecurityTokenValidator>()
    {
      (ISecurityTokenValidator) new Saml2SecurityTokenHandler(),
      //(ISecurityTokenValidator) new SamlSecurityTokenHandler(),
      //(ISecurityTokenValidator) new JwtSecurityTokenHandler()
    };
        private TokenValidationParameters _tokenValidationParameters;


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

        public TokenValidationParameters TokenValidationParameters
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
            this._tokenValidationParameters = new TokenValidationParameters();
            this.BackchannelTimeout = TimeSpan.FromMinutes(1.0);
            this.UseTokenLifetime = true;
            this.RefreshOnIssuerKeyNotFound = true;
        }
    }
}