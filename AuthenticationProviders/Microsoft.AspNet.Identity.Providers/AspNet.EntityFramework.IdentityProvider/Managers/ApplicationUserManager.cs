using AspNet.EntityFramework.IdentityProvider.Models;
using Microsoft.AspNet.Identity;

namespace AspNet.EntityFramework.IdentityProvider.Managers
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store, 
            IUserTokenProvider<ApplicationUser, string> tokenProvider,
            IIdentityValidator<string> passwordValidator,
            IIdentityValidator<ApplicationUser> userValidator)
            : base(store)
        {
            base.UserTokenProvider = tokenProvider;
            base.PasswordValidator = passwordValidator;
            //base.UserValidator = userValidator;
        }
    }
}