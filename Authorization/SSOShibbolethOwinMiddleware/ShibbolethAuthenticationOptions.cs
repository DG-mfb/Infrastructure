using System;
using System.Collections.Generic;
using System.Net.Http;
using Kernel.Authorisation.AuthenticationProvider;
using Microsoft.Owin;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;

namespace SSOShibbolethOwinMiddleware
{
    public class ShibbolethAuthenticationOptions : AuthenticationOptions
    {
        public ICertificateValidator BackchannelCertificateValidator { get; set; }

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

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string AuthorizationEndpoint { get; set; }

        public string TokenEndpoint { get; set; }

        public string UserInformationEndpoint { get; set; }

        public TimeSpan BackchannelTimeout { get; set; }

        public HttpMessageHandler BackchannelHttpHandler { get; set; }

        public IList<string> Scope { get; private set; }

        public PathString CallbackPath { get; set; }

        public string SignInAsAuthenticationType { get; set; }

        public IAuthenticationProvider<ShibbolethAuthenticationOptions> Provider { get; set; }

        public ISecureDataFormat<AuthenticationProperties> StateDataFormat { get; set; }

        public ICookieManager CookieManager { get; set; }

        public ShibbolethAuthenticationOptions()
      : base("Shibboleth")
    {
            this.Caption = "Shibboleth";
            this.CallbackPath = new PathString("/signin-microsoft");
            this.AuthenticationMode = AuthenticationMode.Passive;
            this.Scope = (IList<string>)new List<string>();
            this.BackchannelTimeout = TimeSpan.FromSeconds(60.0);
            this.CookieManager = (ICookieManager)new Microsoft.Owin.Infrastructure.CookieManager();
            this.AuthorizationEndpoint = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize";
            this.TokenEndpoint = "https://login.microsoftonline.com/common/oauth2/v2.0/token";
            this.UserInformationEndpoint = "https://graph.microsoft.com/v1.0/me";
        }
    }
}
