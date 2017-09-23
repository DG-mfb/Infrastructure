using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Kernel.Data;
using Kernel.Data.ORM;
using Kernel.DependancyResolver;
using Kernel.Federation.MetaData.Configuration;
using Kernel.Reflection;
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
            dependencyResolver.RegisterFactory<IMetadataContextBuilder>(() =>
            {
                var models = ReflectionHelper.GetAllTypes(new[] { typeof(MetadataContextBuilder).Assembly })
                .Where(t => !t.IsAbstract && !t.IsInterface && typeof(BaseModel).IsAssignableFrom(t));

                var context = dependencyResolver.Resolve<IDbContext>();
                var contextCustomConfiguration = context as IDbCustomConfiguration;

                contextCustomConfiguration.ModelsFactory = () => models;

                var seeders = ReflectionHelper.GetAllTypes(new[] { typeof(MetadataContextBuilder).Assembly })
                    .Where(t => !t.IsAbstract && !t.IsInterface && typeof(ISeeder).IsAssignableFrom(t))
                    .Select(x => (ISeeder)Activator.CreateInstance(x));
                seeders
                    .OrderBy(x => x.SeedingOrder)
                    .Aggregate(contextCustomConfiguration, (c, next) => { c.Seeders.Add(next); return c; });

                return new MetadataContextBuilder(context);
            }, Lifetime.Transient);

            return Task.CompletedTask;
        }
    }
}