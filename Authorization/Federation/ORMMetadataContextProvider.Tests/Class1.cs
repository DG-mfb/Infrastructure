using Kernel.Data.ORM;
using NUnit.Framework;
using ORMMetadataContextProvider.Tests.Mock;
using Provider.EntityFramework;

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
            object context = new DBContext(connectionStringProvider);
            var metadataContextBuilder = new MetadataContextBuilder((IDbContext)context);
            //ACT
            var metadata = metadataContextBuilder.BuildContext();
            //ASSERT
        }
    }
}