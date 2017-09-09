using System;
using System.IdentityModel.Metadata;
using System.Net.Http;
using System.Net.Security;
using Federation.Protocols.Configuration;
using Kernel.DependancyResolver;
using Kernel.Federation.Protocols;
using Microsoft.Owin;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Infrastructure;
using Owin;
using SSOShibbolethOwinMiddleware.Handlers;
using WsFederationMetadataProvider.Configuration;

namespace SSOShibbolethOwinMiddleware
{
    internal class ShibbolethOwinMiddleware : AuthenticationMiddleware<ShibbolethAuthenticationOptions>
    {
        private readonly ILogger _logger;
        private readonly IDependencyResolver _resolver;
        public ShibbolethOwinMiddleware(OwinMiddleware next, IAppBuilder app, ShibbolethAuthenticationOptions options, IDependencyResolver resolver)
            : base(next, options)
        {
            this._resolver = resolver;
            this._logger = app.CreateLogger<ShibbolethOwinMiddleware>();
            if (base.Options.BackchannelCertificateValidator == null)
            {
                base.Options.BackchannelCertificateValidator = this._resolver.Resolve<Kernel.Federation.CertificateProvider.ICertificateValidator>();
            }

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
            if (this.Options.Configuration != null)
            { }//this.Options.ConfigurationManager = (IConfigurationManager<object>)new StaticConfigurationManager<object>(this.Options.Configuration);
            else
                this.Options.ConfigurationManager = new ConfigurationManager<MetadataBase>(this.Options.MetadataAddress, new WsFederationConfigurationRetriever(), new HttpClient(ShibbolethOwinMiddleware.ResolveHttpMessageHandler(this.Options))
                {
                    Timeout = this.Options.BackchannelTimeout,
                    MaxResponseContentBufferSize = 10485760L
                });
        }
        
        protected override AuthenticationHandler<ShibbolethAuthenticationOptions> CreateHandler()
        {
            return new ShibbolethAccountAuthenticationHandler(this._logger);
        }

        private static HttpMessageHandler ResolveHttpMessageHandler(ShibbolethAuthenticationOptions options)
        {
            HttpMessageHandler httpMessageHandler = options.BackchannelHttpHandler ?? new WebRequestHandler();
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