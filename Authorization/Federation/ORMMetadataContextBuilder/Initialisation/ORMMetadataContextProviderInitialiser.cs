using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;
using System.Threading.Tasks;
using Kernel.DependancyResolver;
using Shared.Initialisation;

namespace ORMMetadataContextProvider.Initialisation
{
    public class ORMMetadataContextProviderInitialiser : Initialiser
    {
        public override byte Order
        {
            get { return 0; }
        }

        protected override Task InitialiseInternal(IDependencyResolver dependencyResolver)
        {
            dependencyResolver.RegisterFactory<Func<PropertyInfo, string>>(() => x => x.Name, Lifetime.Transient);
            //Register connection string dependency in ConnectionDefinitionParser(Func<NameValueCollection> connectionPropertiesFactory, ...)
            dependencyResolver.RegisterFactory<Func<NameValueCollection>>(() => () => ConfigurationManager.AppSettings, Lifetime.Transient);
            return Task.CompletedTask;
        }
    }
}