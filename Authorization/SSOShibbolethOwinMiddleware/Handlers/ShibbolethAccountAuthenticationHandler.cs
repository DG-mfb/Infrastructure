using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;

namespace SSOShibbolethOwinMiddleware.Handlers
{
    internal class ShibbolethAccountAuthenticationHandler : AuthenticationHandler<ShibbolethAuthenticationOptions>
    {
        public override Task<bool> InvokeAsync()
        {
            return base.InvokeAsync();
        }
        protected override Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            return Task.FromResult((AuthenticationTicket)null);
        }
    }
}
