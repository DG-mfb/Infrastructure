using System;
using System.IdentityModel.Metadata;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using WsMetadataSerialisation.Serialisation;

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
            //ToDo:
            //Quick hack to make sure it will redirect to shibboleth and deserialise the metadata
            var request = System.Net.WebRequest.Create(base.Options.MetadataAddress);
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // It may be more efficient to pass the stream directly, but
            // it's likely a bit safer to pull the data off the response
            // stream and create a new memorystream with the data
            MetadataBase metadata;
            using (var ms = new MemoryStream())
            {
                using (var response = request.GetResponse().GetResponseStream())
                {
                    response.CopyTo(ms);
                    response.Close();
                }
                ms.Seek(0, SeekOrigin.Begin); // Rewind memorystream back to the beginning
                                              // We want to allow exceptions to bubble up in this case
                XmlReader reader = XmlReader.Create(ms);
                var ser = new FederationMetadataSerialiser();
                metadata = ser.Deserialise(reader);
            }
            string signInUrl = null;

            var entitiesDescriptors = metadata as EntitiesDescriptor;
            if (entitiesDescriptors != null)
            {
                var idDescpritor = entitiesDescriptors.ChildEntities.SelectMany(x => x.RoleDescriptors)
                    .First(x => x.GetType() == typeof(IdentityProviderSingleSignOnDescriptor)) as IdentityProviderSingleSignOnDescriptor;
                signInUrl = idDescpritor.SingleSignOnServices.FirstOrDefault(x => x.Binding == new Uri("urn:oasis:names:tc:SAML:2.0:bindings:HTTP-Redirect"))
                    .Location.AbsoluteUri;
            }

            var entitityDescriptor = metadata as EntityDescriptor;
            if (entitityDescriptor != null)
            {
                var idDescpritor = entitityDescriptor.RoleDescriptors.Select(x => x)
                    .First(x => x.GetType() == typeof(IdentityProviderSingleSignOnDescriptor)) as IdentityProviderSingleSignOnDescriptor;
                signInUrl = idDescpritor.SingleSignOnServices.FirstOrDefault(x => x.Binding == new Uri("urn:oasis:names:tc:SAML:2.0:bindings:HTTP-Redirect"))
                    .Location.AbsoluteUri;
            }
            //if (this._configuration == null)
            //    this._configuration = await this.Options.ConfigurationManager.GetConfigurationAsync(this.Context.Request.CallCancelled);
            //string baseUri = this.Request.Scheme + Uri.SchemeDelimiter + (object)this.Request.Host + (object)this.Request.PathBase;
            //string currentUri = baseUri + (object)this.Request.Path + (object)this.Request.QueryString;
            //AuthenticationProperties properties = challenge.Properties;
            //if (string.IsNullOrEmpty(properties.RedirectUri))
            //    properties.RedirectUri = currentUri;
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
            this.Response.Redirect(signInUrl);
        }

    }
}