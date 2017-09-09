﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using Kernel.Cryptography.CertificateManagement;
using Kernel.Cryptography.DataProtection;
using Kernel.Extensions;
using Kernel.Federation.Protocols;
using Serialisation.Xml;

namespace Federation.Protocols.Request
{
    public class AuthnRequestBuilder : IAuthnRequestBuilder
    {
        private ICertificateManager _certificateManager;

        public AuthnRequestBuilder(ICertificateManager certificateManager)
        {
            this._certificateManager = certificateManager;
        }

        public Uri BuildRedirectUri(AuthnRequestContext authnRequestContext)
        {
            var configuration = authnRequestContext.Configuration;
            var authnRequest = new AuthnRequest
            {
                Id = "http://localhost:60879/sp/metadata",
                IsPassive = false,
                Destination = authnRequestContext.Destination.AbsoluteUri,
                Version = "2.0",
                IssueInstant = DateTime.UtcNow
            };
            authnRequest.Issuer = new NameId { Value = "http://localhost:60879/sp/metadata" };
            var audienceRestrictions = new List<ConditionAbstract>();
            var audienceRestriction = new AudienceRestriction { Audience = new List<string>() { "http://localhost:60879/sp/metadata" } };
            audienceRestrictions.Add(audienceRestriction);

            authnRequest.Conditions = new Conditions { Items = audienceRestrictions };
            
            var serialiser = new XMLSerialiser();
            
            serialiser.XmlNamespaces.Add("samlp", Saml20Constants.Protocol);
            serialiser.XmlNamespaces.Add("saml", Saml20Constants.Assertion);
            var sb = new StringBuilder();
            using (var ms = new MemoryStream())
            {
                serialiser.Serialize(ms, new[] { authnRequest });
                ms.Position = 0;
                var streamReader = new StreamReader(ms);
                var xmlString = streamReader.ReadToEnd();
                ms.Position = 0;
                var encoded = this.DeflateEncode(xmlString);
                var encodedEscaped = Uri.EscapeDataString(encoded);
                sb.Append(encodedEscaped);
                var signed = this.SignRequest(sb);
                var result = authnRequest.Destination + "?SAMLRequest=" + sb.ToString();
                return new Uri(result);
            }
        }

        private string SignRequest(StringBuilder sb)
        {
            //ToDo:
            var cert = this._certificateManager.GetCertificate(@"D:\Dan\Software\SGWI\ThirdParty\devCertsPackage\employeeportaldev.safeguardworld.com.pfx", StringExtensions.ToSecureString("$Password1!"));
            sb.AppendFormat("&{0}={1}", HttpRedirectBindingConstants.SigAlg, Uri.EscapeDataString(SignedXml.XmlDsigRSASHA1Url));
            var signed = RSADataProtection.SignDataSHA1((RSA)cert.PrivateKey, Encoding.UTF8.GetBytes(sb.ToString()));
            //var isValid = RSADataProtection.VerifyDataSHA1Signed((RSA)cert.PrivateKey, Encoding.UTF8.GetBytes(sb.ToString()), signed);
            var base64 = Convert.ToBase64String(signed);
            var escaped = Uri.EscapeDataString(base64);
            sb.AppendFormat("&{0}={1}", HttpRedirectBindingConstants.Signature, escaped);
            return sb.ToString();
        }
        private string DeflateEncode(string val)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new StreamWriter(new DeflateStream(memoryStream, CompressionMode.Compress, true), new UTF8Encoding(false)))
                {
                    writer.Write(val);
                    writer.Close();

                    return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length, Base64FormattingOptions.None);
                }
            }
        }
    }
}