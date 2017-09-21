using Kernel.Data.ORM;
using System.Linq;
using Kernel.Reflection;
using NUnit.Framework;
using ORMMetadataContextProvider.Tests.Mock;
using Provider.EntityFramework;
using Kernel.Data;

namespace ORMMetadataContextProvider.Tests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void Test1()
        {
            //ARRANGE
            var connectionStringProvider = new ConnectionStringProviderMock();
            var models = ReflectionHelper.GetAllTypes(new[] {typeof(MetadataContextBuilder).Assembly })
                .Where(t => !t.IsAbstract && !t.IsInterface && typeof(BaseModel).IsAssignableFrom(t));
            object context = new DBContext(connectionStringProvider) { ModelsFactory = () => models };
            var metadataContextBuilder = new MetadataContextBuilder((IDbContext)context);
            //ACT
            var metadata = metadataContextBuilder.BuildContext();
            //ASSERT
        }
    }
}