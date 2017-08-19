using System.Threading.Tasks;
using Kernel.DependancyResolver;
using Shared.Initialisation;

namespace AspNet.EntityFramework.IdentityProvider.Initialisation
{
    public class IdentityInitialiser : Initialiser
    {
        public override byte Order
        {
            get { return 0; }
        }

        protected override Task InitialiseInternal(IDependencyResolver dependencyResolver)
        {
            
            return Task.CompletedTask;
        }
    }
}