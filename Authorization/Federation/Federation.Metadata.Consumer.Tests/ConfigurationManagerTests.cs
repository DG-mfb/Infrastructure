using System.IdentityModel.Metadata;
using System.Threading.Tasks;
using Federation.Metadata.Consumer.Configuration;
using Federation.Metadata.Consumer.Tests.Mock;
using Kernel.Federation.MetadataConsumer;
using NUnit.Framework;

namespace Federation.Metadata.Consumer.Tests
{
    [TestFixture]
    internal class ConfigurationManagerTests
    {
        [Test]
        public async Task ManagerTest()
        {
            //ARRANGE
            MetadataBase configuration = null;
            var configurationRetriever = new ConfigurationRetrieverMock();
            var context = new MetadataConsumerContext("C:\\");
            var configurationManager = new ConfigurationManager<MetadataBase>(context, configurationRetriever);
            //ACT
            configuration = await configurationManager.GetConfigurationAsync();
            //ASSET
            Assert.IsNotNull(configuration);
        }
    }
}