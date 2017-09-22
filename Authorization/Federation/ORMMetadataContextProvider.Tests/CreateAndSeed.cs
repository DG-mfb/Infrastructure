using Kernel.Data.ORM;
using System.Linq;
using Kernel.Reflection;
using NUnit.Framework;
using ORMMetadataContextProvider.Tests.Mock;
using Provider.EntityFramework;
using Kernel.Data;
using ORMMetadataContextProvider.Models.GlobalConfiguration;
using ORMMetadataContextProvider.Seeders;
using System;

namespace ORMMetadataContextProvider.Tests
{
    [TestFixture]
    public class CreateAndSeed
    {
        [Test]
        public void Test1()
        {
            //ARRANGE
            
            var connectionStringProvider = new MetadataConnectionStringProviderMock();
            var models = ReflectionHelper.GetAllTypes(new[] {typeof(MetadataContextBuilder).Assembly })
                .Where(t => !t.IsAbstract && !t.IsInterface && typeof(BaseModel).IsAssignableFrom(t));
            object context = new DBContext(connectionStringProvider) { ModelsFactory = () => models };

            var seeders = ReflectionHelper.GetAllTypes(new[] { typeof(MetadataContextBuilder).Assembly })
                .Where(t => !t.IsAbstract && !t.IsInterface && typeof(ISeeder).IsAssignableFrom(t))
                .Select(x => (ISeeder)Activator.CreateInstance(x));
            seeders.Aggregate((IDbCustomConfiguration)context, (c, next) => { c.Seeders.Add(next); return c; });
            

            var metadataContextBuilder = new MetadataContextBuilder((IDbContext)context);
            //var metadata = metadataContextBuilder.BuildContext();
            //temp
            //var connectionStringProvider1 = new GlobalConnectionStringProviderMock();
            //var models1 = new[] { typeof(SecuritySettings) };
            //object context1 = new DBContext(connectionStringProvider1) { ModelsFactory = () => models1 };
            //var foo = ((IDbContext)context1).Set<SecuritySettings>().ToList();
            //ACT
            var metadata = metadataContextBuilder.BuildContext();
            //ASSERT
        }
    }
}