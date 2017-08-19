using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Kernel.DependancyResolver;
using Microsoft.Owin.Security.DataProtection;
using Shared.Initialisation;

namespace Microsoft.AspNet.Identity.Owin.Provider.Initialisation
{
    public class IdentityInitialiser : Initialiser
    {
        public override byte Order
        {
            get { return 0; }
        }

        protected override Task InitialiseInternal(IDependencyResolver dependencyResolver)
        {
            dependencyResolver.RegisterFactory(typeof(IUserTokenProvider<,>), t =>
            {
                var targetType = typeof(DataProtectorTokenProvider<,>)
                .MakeGenericType(t, typeof(string));
                var dataProtector = new DpapiDataProtectionProvider().Create("OwinIdentity");
                var ctr = targetType.GetConstructor(new Type[] { typeof(IDataProtector) });
                var par = Expression.Parameter(typeof(IDataProtector));
                var newExp = Expression.New(ctr, par);
                var lambda = Expression.Lambda(newExp, par).Compile();
                return lambda.DynamicInvoke(dataProtector);
            }, Lifetime.Transient);

            return Task.CompletedTask;
        }
    }
}