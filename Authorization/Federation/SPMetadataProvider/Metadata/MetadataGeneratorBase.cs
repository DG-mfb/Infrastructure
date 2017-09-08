using System;
using System.Collections.Generic;
using System.IdentityModel.Metadata;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Kernel.Cryptography.CertificateManagement;
using Kernel.Cryptography.Signing.Xml;
using Kernel.Federation.MetaData;
using WsFederationMetadataProvider.Metadata.DescriptorBuilders;

namespace WsFederationMetadataProvider.Metadata
{
    public abstract class MetadataGeneratorBase : IMetadataGenerator
    {
        protected IFederationMetadataWriter _federationMetadataWriter;

        protected readonly ICertificateManager _certificateManager;
        protected readonly IXmlSignatureManager _xmlSignatureManager;
        protected readonly IMetadataSerialiser<MetadataBase> _serialiser;
        protected readonly Func<IMetadataGenerator, IMetadataConfiguration> _configuration;
        public MetadataGeneratorBase(IFederationMetadataWriter federationMetadataWriter, ICertificateManager certificateManager, IXmlSignatureManager xmlSignatureManager, IMetadataSerialiser<MetadataBase> serialiser, Func<IMetadataGenerator, IMetadataConfiguration> configuration)
        {
            this._federationMetadataWriter = federationMetadataWriter;
            this._certificateManager = certificateManager;
            this._xmlSignatureManager = xmlSignatureManager;
            this._serialiser = serialiser;
            this._configuration = configuration;
        }

        public Task CreateMetadata()
        {
            var configuration = this._configuration(this);
            return ((IMetadataGenerator)this).CreateMetadata(configuration);
        }

        Task IMetadataGenerator.CreateMetadata(IMetadataConfiguration configuration)
        {
            try
            {
                var descriptors = this.GetDescriptors(configuration);
                foreach (var descriptor in descriptors)
                {
                    ProcessKeys(configuration, descriptor);
                }
                var entityDescriptor = BuildEntityDesciptor(configuration, descriptors);
                
                var sb = new StringBuilder();
                
                using (var xmlWriter = XmlWriter.Create(sb))
                {
                    this._serialiser.Serialise(xmlWriter, entityDescriptor);
                }

                var metadata = new XmlDocument();
                metadata.LoadXml(sb.ToString());

                this.SignMetadata(configuration, metadata.DocumentElement);

                this._federationMetadataWriter.Write(metadata.DocumentElement, configuration);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ProcessKeys(IMetadataConfiguration configuration, RoleDescriptor Descriptor)
        {
            foreach (var key in configuration.Keys)
            {
                var certificate = this._certificateManager.GetCertificate(key.SertificateFilePath, key.CertificatePassword);

                var keyDescriptor = new KeyDescriptor();
                KeyType keyType;
                if (!Enum.TryParse<KeyType>(key.Usage, out keyType))
                {
                    throw new InvalidCastException(String.Format("Parsing to type{0} failed. Value having been tried:{1}", typeof(KeyType), key.Usage));
                }

                keyDescriptor.Use = keyType;

                keyDescriptor.KeyInfo = new SecurityKeyIdentifier(new X509IssuerSerialKeyIdentifierClause(certificate));

                Descriptor.Keys.Add(keyDescriptor);
            }
        }

        protected void SignMetadata(IMetadataConfiguration configuration, XmlElement xml)
        {
        var signMetadataKey = configuration.Keys.Where(k => k.DefaultForMetadataSigning)
                .FirstOrDefault();

            if (signMetadataKey == null)
                throw new Exception("No default certificate found");

            var certificate = this._certificateManager.GetCertificate(signMetadataKey.SertificateFilePath, signMetadataKey.CertificatePassword);

            this._xmlSignatureManager.Generate(xml, certificate.PrivateKey, null, certificate, null, null, null);
        }

        protected virtual EntityDescriptor BuildEntityDesciptor(IMetadataConfiguration configuration, IEnumerable<RoleDescriptor> descriptors)
        {
            var entityDescriptor = new EntityDescriptor()
            {
                EntityId = new EntityId(configuration.EntityId.AbsoluteUri),
                FederationId = "84CCAA9F05EE4BA1B13F8943FDF1D320"
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
        protected virtual IEnumerable<RoleDescriptor> GetDescriptors(IMetadataConfiguration configuration)
        {
            if (configuration.Descriptors == null || configuration.Descriptors.Count() == 0)
            {
                throw new InvalidOperationException("No descriptors provided.");
            }
            var descriptors = new List<RoleDescriptor>();
            configuration.Descriptors.Aggregate(descriptors, (agg, next) =>
            {
                var descriptor = DescriptorBuildersHelper.ResolveAndBuild(next.DescriptorType, configuration);
                agg.Add(descriptor);
                return agg;
            });
            return descriptors;
        }
    }
}