using System.Linq;
using InlineMetadataContextProvider;
using Kernel.Federation.MetaData.Configuration.RoleDescriptors;
using NUnit.Framework;
using WsFederationMetadataProvider.Metadata.DescriptorBuilders;
using WsFederationMetadataProviderTests.Mock;

namespace WsFederationMetadataProviderTests
{
    [TestFixture]
    internal class DescriptorBuildersTests
    {
        [Test]
        public void ServiceProviderSingleSignOnDescriptorBuilderTest()
        {
            //ARRANGE
            var contextBuilder = new InlineMetadataContextBuilder();
            var context = contextBuilder.BuildContext();
            var spDescriptorConfigurtion = context.EntityDesriptorConfiguration.RoleDescriptors.First() as SPSSODescriptorConfiguration;
            var descriptorBuilder = new ServiceProviderSingleSignOnDescriptorBuilder();
            //ACT
            var descriptor = descriptorBuilder.BuildDescriptor(spDescriptorConfigurtion);
            //ASSERT
            Assert.AreEqual(descriptor.ValidUntil, spDescriptorConfigurtion.ValidUntil);
            Assert.AreEqual(descriptor.WantAssertionsSigned, spDescriptorConfigurtion.WantAssertionsSigned);
            Assert.True(Enumerable.SequenceEqual(descriptor.ProtocolsSupported, spDescriptorConfigurtion.ProtocolSupported));
        }
    }
}