using System.Threading.Tasks;
using Kernel.DependancyResolver;
using Shared.Initialisation;

namespace ORMMetadataContextProvider.Initialisation
{
    public class ORMMetadataContextProviderInitialiser : Initialiser
    {
        public override byte Order
        {
            get { return 1; }
        }

        protected override Task InitialiseInternal(IDependencyResolver dependencyResolver)
        {
            return Task.CompletedTask;
        }
    }
}