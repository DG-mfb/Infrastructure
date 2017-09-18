using Kernel.Federation.MetaData.Configuration;

namespace InlineMetadataContextProvider
{
    internal class InlineMetadataContextBuilder : IMetadataContextBuilder
    {
        public MetadataContext BuildContext()
        {
            var entityDescriptorConfiguration = MetadataHelper.BuildEntityDesriptorConfiguration();

            var keyDescriptorConfiguration = MetadataHelper.BuildKeyDescriptorConfiguration();
            entityDescriptorConfiguration.KeyDescriptors.Add(keyDescriptorConfiguration);

            var spDescriptorConfigurtion = MetadataHelper.BuildSPSSODescriptorConfiguration();
            entityDescriptorConfiguration.RoleDescriptors.Add(spDescriptorConfigurtion);
            
            var context = new MetadataContext
            {
                EntityDesriptorConfiguration = entityDescriptorConfiguration,
                SignMetadata = true
            };

            context.KeyDescriptors.Add(keyDescriptorConfiguration);
            return context;
        }
    }
}