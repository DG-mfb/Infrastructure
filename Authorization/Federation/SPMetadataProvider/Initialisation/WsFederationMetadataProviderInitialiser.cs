using System;
using System.Threading.Tasks;
using Kernel.DependancyResolver;
using Kernel.Federation.MetaData;
using Shared.Initialisation;
using WsFederationMetadataProvider.Metadata;

namespace WsFederationMetadataProvider.Initialisation
{
    public class WsFederationMetadataProviderInitialiser : Initialiser
    {
        public override byte Order
        {
            get { return 1; }
        }

        protected override Task InitialiseInternal(IDependencyResolver dependencyResolver)
        {
            dependencyResolver.RegisterType<SPSSOMetadataProvider>(Lifetime.Transient);
            dependencyResolver.RegisterType<IdpSSOMetadataProvider>(Lifetime.Transient);
            dependencyResolver.RegisterFactory<Func<IMetadataGenerator, IMetadataConfiguration>>(t =>
            {
                return _ =>
                {
                    throw new NotImplementedException();
                };
            }, Lifetime.Singleton);
            return Task.CompletedTask;
        }
    }
}