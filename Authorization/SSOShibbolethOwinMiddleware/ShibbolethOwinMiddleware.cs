using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;
using Owin;
using SSOShibbolethOwinMiddleware.Handlers;

namespace SSOShibbolethOwinMiddleware
{
    internal class ShibbolethOwinMiddleware : AuthenticationMiddleware<ShibbolethAuthenticationOptions>
    {
        private readonly IAppBuilder _app;
        public ShibbolethOwinMiddleware(OwinMiddleware next, IAppBuilder app, ShibbolethAuthenticationOptions options) : base(next, options)
        {
            this._app = app;
        }
        
        protected override AuthenticationHandler<ShibbolethAuthenticationOptions> CreateHandler()
        {
            return new ShibbolethAccountAuthenticationHandler();
        }
    }
}