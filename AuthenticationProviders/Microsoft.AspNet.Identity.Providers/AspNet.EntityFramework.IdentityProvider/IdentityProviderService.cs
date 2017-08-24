using System;
using System.Threading.Tasks;
using AspNet.EntityFramework.IdentityProvider.Managers;
using Kernel.Authentication.Services;

namespace AspNet.EntityFramework.IdentityProvider
{
    internal class IdentityProviderService : IIdentityProviderService
    {
        private readonly ApplicationUserManager _manager;

        public IdentityProviderService(ApplicationUserManager manager)
        {
            this._manager = manager;
        }
        public async Task<AuthenticationResult> AuthenticateUser(IdentityAuthenicationContext context)
        {
            var user = await this._manager.FindAsync(context.UserName, context.Password);
            return new AuthenticationResult(user != null ? Kernel.Authentication.AuthenticationResults.Success : Kernel.Authentication.AuthenticationResults.FailInvalidCredentials);
        }
    }
}