using System;
using System.Threading.Tasks;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;

namespace SSOShibbolethOwinMiddleware.Handlers
{
    internal class ShibbolethAccountAuthenticationHandler : AuthenticationHandler<ShibbolethAuthenticationOptions>
    {
        private const string HandledResponse = "HandledResponse";
        private readonly ILogger _logger;
        private object _configuration;

        public ShibbolethAccountAuthenticationHandler(ILogger logger)
        {
            this._logger = logger;
        }

        public override Task<bool> InvokeAsync()
        {
            return base.InvokeAsync();
        }
        protected override Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            return Task.FromResult((AuthenticationTicket)null);
        }
        protected override async Task ApplyResponseChallengeAsync()
        {
            if (this.Response.StatusCode != 401)
                return;
            AuthenticationResponseChallenge challenge = this.Helper.LookupChallenge(this.Options.AuthenticationType, this.Options.AuthenticationMode);
            if (challenge == null)
                return;
            if (this._configuration == null)
                this._configuration = await this.Options.ConfigurationManager.GetConfigurationAsync(this.Context.Request.CallCancelled);
            string baseUri = this.Request.Scheme + Uri.SchemeDelimiter + (object)this.Request.Host + (object)this.Request.PathBase;
            string currentUri = baseUri + (object)this.Request.Path + (object)this.Request.QueryString;
            AuthenticationProperties properties = challenge.Properties;
            if (string.IsNullOrEmpty(properties.RedirectUri))
                properties.RedirectUri = currentUri;
            //WsFederationMessage federationMessage = new WsFederationMessage();
            //federationMessage.IssuerAddress = this._configuration.TokenEndpoint ?? string.Empty;
            //federationMessage.Wtrealm = this.Options.Wtrealm;
            //federationMessage.Wctx = "WsFedOwinState=" + Uri.EscapeDataString(this.Options.StateDataFormat.Protect(properties));
            //federationMessage.Wa = "wsignin1.0";
            //WsFederationMessage wsFederationMessage = federationMessage;
            //if (!string.IsNullOrWhiteSpace(this.Options.Wreply))
            //    wsFederationMessage.Wreply = this.Options.Wreply;
            //RedirectToIdentityProviderNotification<WsFederationMessage, WsFederationAuthenticationOptions> notification = new RedirectToIdentityProviderNotification<WsFederationMessage, WsFederationAuthenticationOptions>(this.Context, this.Options)
            //{
            //    ProtocolMessage = wsFederationMessage
            //};
            //await this.Options.Notifications.RedirectToIdentityProvider(notification);
            //if (notification.HandledResponse)
            //    return;
            //string signInUrl = notification.ProtocolMessage.CreateSignInUrl();
            //if (!Uri.IsWellFormedUriString(signInUrl, UriKind.Absolute))
            //    this._logger.WriteWarning("The sign-in redirect URI is malformed: " + signInUrl);
            //this.Response.Redirect(signInUrl);
        }

    }
}