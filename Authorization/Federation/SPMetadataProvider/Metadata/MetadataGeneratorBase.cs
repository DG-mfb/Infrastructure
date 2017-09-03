﻿using System;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Xml;
using Kernel.Cryptography.CertificateManagement;
using Kernel.Cryptography.Signing.Xml;
using Kernel.Federation.MetaData;
using Microsoft.IdentityModel.Protocols.WSFederation.Metadata;
using WsFederationMetadataProvider.Serialisation;

namespace WsFederationMetadataProvider.Metadata
{
    public abstract class MetadataGeneratorBase<T> : IMetadataGenerator where T : RoleDescriptor
    {
        protected IFederationMetadataWriter _federationMetadataWriter;

        protected ICertificateManager _certificateManager;
        protected IXmlSignatureManager _xmlSignatureManager;

        protected abstract Action<EntityDescriptor, T> AssignmentAction { get; }

        public MetadataGeneratorBase(IFederationMetadataWriter federationMetadataWriter, ICertificateManager certificateManager, IXmlSignatureManager xmlSignatureManager)
        {
            this._federationMetadataWriter = federationMetadataWriter;
            this._certificateManager = certificateManager;
            this._xmlSignatureManager = xmlSignatureManager;
        }

        public void CreateMetadata(IMetadataConfiguration configuration)
        {
            try
            {
                var descriptor = GetDescriptor(configuration);

                ProcessKeys(configuration, descriptor);

                var entityDescriptor = BuildEntityDesciptor(configuration, descriptor);

                var ser = new FederationMetadataSerialiser();
                var sb = new StringBuilder();
                
                using (var xmlWriter = XmlWriter.Create(sb))
                {
                    ser.WriteMetadata(xmlWriter, entityDescriptor);
                }
                
                var metadata = new XmlDocument();
                metadata.LoadXml(sb.ToString());

                SignMetadata(configuration, metadata.DocumentElement);

                _federationMetadataWriter.Write(metadata.DocumentElement, configuration);
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

                var keyDescriptor = new Microsoft.IdentityModel.Protocols.WSFederation.Metadata.KeyDescriptor();
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

        protected virtual EntityDescriptor BuildEntityDesciptor(IMetadataConfiguration configuration, RoleDescriptor descriptor)
        {
            var entityDescriptor = new EntityDescriptor()
            {
                EntityId = new EntityId(configuration.EntityId.AbsoluteUri),
                FederationId = "84CCAA9F05EE4BA1B13F8943FDF1D320"
            };

            AssignmentAction(entityDescriptor, (T)descriptor);

            return entityDescriptor;
        }

        protected abstract RoleDescriptor GetDescriptor(IMetadataConfiguration configuration);
    }
}