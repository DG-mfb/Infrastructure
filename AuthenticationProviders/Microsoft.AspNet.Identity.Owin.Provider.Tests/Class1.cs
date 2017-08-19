using AspNet.EntityFramework.IdentityProvider.Models;
using Microsoft.AspNet.Identity.Owin.Provider.Factories;
using Microsoft.Owin.Security.DataProtection;
using NUnit.Framework;

namespace Microsoft.AspNet.Identity.Owin.Provider.Tests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void Test1()
        {
            var dataProtector = new DpapiDataProtectionProvider().Create("OwinIdentity");
            var del = UserTokenProviderFactory.GetTokenProviderDelegate(typeof(ApplicationUser));
            var r = del(dataProtector);
        }
    }
}
