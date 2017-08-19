using System;
using AspNet.EntityFramework.IdentityProvider.Models;
using Microsoft.AspNet.Identity;

namespace AspNet.EntityFramework.IdentityProvider.Managers
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager
            (
            IUserStore<ApplicationUser> store, 
            IUserTokenProvider<ApplicationUser, string> tokenProvider,
            IIdentityValidator<string> passwordValidator,
            Func<UserManager<ApplicationUser>, IIdentityValidator<ApplicationUser>> userValidatorFactory
            )
            : base(store)
        {
            base.UserTokenProvider = tokenProvider;
            base.PasswordValidator = passwordValidator;
            base.UserValidator = userValidatorFactory(this);
        }
    }
}