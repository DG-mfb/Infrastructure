using AspNet.EntityFramework.IdentityProvider.Managers;
using AspNet.EntityFramework.IdentityProvider.Models;
using Kernel.DependancyResolver;
using Microsoft.AspNet.Identity.Owin.Provider.Factories;
using Microsoft.Owin.Security.DataProtection;
using NUnit.Framework;
using UnityResolver;

namespace Microsoft.AspNet.Identity.Owin.Provider.Tests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void Test1()
        {
            var resolver = new UnityDependencyResolver();
            resolver.RegisterType<ApplicationUserManager>(Lifetime.Transient);
            //resolver.RegisterType<IUserStore<ApplicationUser>>(Lifetime.Transient);
            resolver.RegisterFactory(typeof(IUserTokenProvider<,>), t =>
            {
               
                var dataProtector = new DpapiDataProtectionProvider().Create("OwinIdentity");
                var del = UserTokenProviderFactory.GetTokenProviderDelegate(typeof(ApplicationUser));
                var r = del(dataProtector);
                return r;
            }, Lifetime.Transient);

            //var dataProtector = new DpapiDataProtectionProvider().Create("OwinIdentity");

            var manager = resolver.Resolve<ApplicationUserManager>();
        }
    }
}