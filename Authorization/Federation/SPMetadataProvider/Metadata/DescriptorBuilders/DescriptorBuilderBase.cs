using System;
using System.IdentityModel.Metadata;
using System.IdentityModel.Tokens;
using Kernel.Federation.MetaData;
using Kernel.Federation.MetaData.Configuration.RoleDescriptors;

namespace WsFederationMetadataProvider.Metadata.DescriptorBuilders
{
    internal abstract class DescriptorBuilderBase<TRole> : IDescriptorBuilder<TRole> where TRole : RoleDescriptor
    {
        public TRole BuildDescriptor(RoleDescriptorConfiguration configuration)
        {
            var descriptor = this.BuildDescriptorInternal(configuration);
            this.BuildKeys(configuration, descriptor);
            return descriptor;
        }

        protected virtual void BuildKeys(RoleDescriptorConfiguration configuration, TRole descriptor)
        {
            foreach (var key in configuration.KeyDescriptors)
            {
                var certificate = key.KeyInfo.GetX509Certificate2();

                var keyDescriptor = new KeyDescriptor();
                KeyType keyType;
                if (!Enum.TryParse<KeyType>(key.Use.ToString(), out keyType))
                {
                    throw new InvalidCastException(String.Format("Parsing to type{0} failed. Value having been tried:{1}", typeof(KeyType), key.Use));
                }

                keyDescriptor.Use = keyType;

                keyDescriptor.KeyInfo = new SecurityKeyIdentifier(new X509RawDataKeyIdentifierClause(certificate));

                descriptor.Keys.Add(keyDescriptor);
            }
        }

        protected abstract TRole BuildDescriptorInternal(RoleDescriptorConfiguration configuration);
    }
}