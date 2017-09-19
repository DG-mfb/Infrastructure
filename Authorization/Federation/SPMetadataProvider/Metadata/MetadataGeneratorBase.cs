using System;
using System.Collections.Generic;
using System.IdentityModel.Metadata;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Kernel.Cryptography.CertificateManagement;
using Kernel.Federation.MetaData;
using Kernel.Federation.MetaData.Configuration;
using Kernel.Federation.MetaData.Configuration.Cryptography;
using Kernel.Federation.MetaData.Configuration.EntityDescriptors;
using Kernel.Federation.MetaData.Configuration.RoleDescriptors;
using WsFederationMetadataProvider.Metadata.DescriptorBuilders;

namespace WsFederationMetadataProvider.Metadata
{
    public abstract class MetadataGeneratorBase : IMetadataGenerator
    {
        protected IFederationMetadataWriter _federationMetadataWriter;

        protected readonly ICertificateManager _certificateManager;
        protected readonly IMetadataSerialiser<MetadataBase> _serialiser;
        protected readonly Func<MetadataType , MetadataContext> _contextFactory;
        public MetadataGeneratorBase(IFederationMetadataWriter federationMetadataWriter, ICertificateManager certificateManager, IMetadataSerialiser<MetadataBase> serialiser, Func<MetadataType, MetadataContext> contextFactory)
        {
            this._federationMetadataWriter = federationMetadataWriter;
            this._certificateManager = certificateManager;
            this._serialiser = serialiser;
            this._contextFactory = contextFactory;
        }

        public Task CreateMetadata(MetadataType metadataType)
        {
            var configuration = this._contextFactory(metadataType);
            return ((IMetadataGenerator)this).CreateMetadata(configuration);
        }

        Task IMetadataGenerator.CreateMetadata(MetadataContext metadataContext)
        {
            try
            {
                var configuration = metadataContext.EntityDesriptorConfiguration;

                var descriptors = this.GetDescriptors(configuration.SPSSODescriptors);
                
                var entityDescriptor = BuildEntityDesciptor(configuration, descriptors);
                this.SignMetadata(metadataContext, entityDescriptor);
                var sb = new StringBuilder();
                
                using (var xmlWriter = XmlWriter.Create(sb))
                {
                    this._serialiser.Serialise(xmlWriter, entityDescriptor);
                }

                var metadata = new XmlDocument();
                metadata.LoadXml(sb.ToString());

                this._federationMetadataWriter.Write(metadata.DocumentElement);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void SignMetadata(MetadataContext context, MetadataBase metadata)
        {
            if (!context.SignMetadata)
                return;
            var signMetadataKey = context.KeyDescriptors.Where(k => k.IsDefault && (k.KeyTarget & KeyTarget.MetaData) == KeyTarget.MetaData)
                    .FirstOrDefault();

            if (signMetadataKey == null)
                throw new Exception("No default certificate found");
            var certConfiguration = new X509StoreCertificateConfiguration(signMetadataKey.CertificateContext);
            var certificate = this._certificateManager.GetCertificate(certConfiguration);
            var signingCredentials = new SigningCredentials(new X509AsymmetricSecurityKey(certificate), SecurityAlgorithms.RsaSha1Signature, SecurityAlgorithms.Sha1Digest, new SecurityKeyIdentifier(new X509RawDataKeyIdentifierClause(certificate)));
            metadata.SigningCredentials = signingCredentials;
        }

        protected virtual EntityDescriptor BuildEntityDesciptor(EntityDesriptorConfiguration configuration, IEnumerable<RoleDescriptor> descriptors)
        {
            var entityDescriptor = new EntityDescriptor()
            {
                EntityId = new EntityId(configuration.EntityId),
                FederationId = configuration.Id
            };

            descriptors.Aggregate(entityDescriptor, (ed, next) =>
            {
                Assignment()(entityDescriptor, next);
                return ed;
            });

            return entityDescriptor;
        }

        protected virtual Action<EntityDescriptor, RoleDescriptor> Assignment()
        {
            return (ed, rd) => ed.RoleDescriptors.Add(rd);
        }
        protected virtual IEnumerable<RoleDescriptor> GetDescriptors(IEnumerable<RoleDescriptorConfiguration> configurations)
        {
            if (configurations == null || configurations.Count() == 0)
            {
                throw new InvalidOperationException("No descriptors provided.");
            }
            var descriptors = new List<RoleDescriptor>();
            configurations.Aggregate(descriptors, (agg, next) =>
            {
                var descriptor = DescriptorBuildersHelper.ResolveAndBuild(next);
                agg.Add(descriptor);
                return agg;
            });
            return descriptors;
        }
    }
}