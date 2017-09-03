﻿using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;
using Kernel.Cryptography.Signing.Xml;
using SecurityManagement.DefaultSigning;

namespace SecurityManagement
{
    public class XmlSignatureManager : IXmlSignatureManager
    {
        public KeyInfo CreateKeyInfo(X509Certificate2 certificate)
        {
            var keyData = new KeyInfoX509Data(certificate);

            var keyInfo = new KeyInfo();

            keyInfo.AddClause(keyData);

            if (certificate.HasPrivateKey)
            {
                var rsa = new RSAKeyValue((RSA)certificate.PrivateKey);

                keyInfo.AddClause(rsa);
            }

            return keyInfo; ;
        }

        public void Generate(XmlElement xmlElement, AsymmetricAlgorithm signingKey, KeyInfo keyInfo, X509Certificate2 x509Certificate, string inclusiveNamespacesPrefixList, string digestMethod, string signatureMethod)
        {
            SAMLMetadataSignature.Generate(xmlElement, signingKey, x509Certificate);
        }
    }
}