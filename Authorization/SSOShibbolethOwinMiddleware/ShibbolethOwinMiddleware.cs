using System;
using System.Net.Http;
using System.Net.Security;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Infrastructure;
using Owin;
using SSOShibbolethOwinMiddleware.Handlers;

namespace SSOShibbolethOwinMiddleware
{
    internal class ShibbolethOwinMiddleware : AuthenticationMiddleware<ShibbolethAuthenticationOptions>
    {
        private readonly IAppBuilder _app;
        public ShibbolethOwinMiddleware(OwinMiddleware next, IAppBuilder app, ShibbolethAuthenticationOptions options)
            : base(next, options)
        {
            //this._logger = app.CreateLogger<WsFederationAuthenticationMiddleware>();
            if (string.IsNullOrWhiteSpace(this.Options.TokenValidationParameters.AuthenticationType))
                this.Options.TokenValidationParameters.AuthenticationType = app.GetDefaultSignInAsAuthenticationType();
            if (this.Options.StateDataFormat == null)
                this.Options.StateDataFormat = (ISecureDataFormat<AuthenticationProperties>)new PropertiesDataFormat(app.CreateDataProtector(typeof(ShibbolethOwinMiddleware).FullName, this.Options.AuthenticationType, "v1"));
            //if (this.Options.Notifications == null)
            //    this.Options.Notifications = new WsFederationAuthenticationNotifications();
            Uri result;
            if (!this.Options.CallbackPath.HasValue && !string.IsNullOrEmpty(this.Options.Wreply) && Uri.TryCreate(this.Options.Wreply, UriKind.Absolute, out result))
                this.Options.CallbackPath = PathString.FromUriComponent(result);
            if (this.Options.ConfigurationManager != null)
                return;
            //if (this.Options.Configuration != null)
            //    this.Options.ConfigurationManager = (IConfigurationManager<object>)new StaticConfigurationManager<object>(this.Options.Configuration);
            //else
            //    this.Options.ConfigurationManager = (IConfigurationManager<object>)new ConfigurationManager<object>(this.Options.MetadataAddress, (IConfigurationRetriever<object>)new WsFederationConfigurationRetriever(), new HttpClient(WsFederationAuthenticationMiddleware.ResolveHttpMessageHandler(this.Options))
            //    {
            //        Timeout = this.Options.BackchannelTimeout,
            //        MaxResponseContentBufferSize = 10485760L
            //    });
        }
        
        protected override AuthenticationHandler<ShibbolethAuthenticationOptions> CreateHandler()
        {
            return new ShibbolethAccountAuthenticationHandler();
        }

        private static HttpMessageHandler ResolveHttpMessageHandler(ShibbolethAuthenticationOptions options)
        {
            HttpMessageHandler httpMessageHandler = options.BackchannelHttpHandler ?? (HttpMessageHandler)new WebRequestHandler();
            if (options.BackchannelCertificateValidator != null)
            {
                WebRequestHandler webRequestHandler = httpMessageHandler as WebRequestHandler;
                if (webRequestHandler == null)
                    throw new InvalidOperationException();
                webRequestHandler.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(options.BackchannelCertificateValidator.Validate);
            }
            return httpMessageHandler;
        }
    }
}