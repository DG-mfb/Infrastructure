using System.Linq;
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
            var spDescriptorConfigurtion = MetadataHelper.BuildSPSSODescriptorConfiguration();
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