using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using Kernel.Federation.Protocols;
using Serialisation.Xml;

namespace Federation.Protocols.Request
{
    public class AuthnRequestBuilder : IAuthnRequestBuilder
    {
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
            var audienceRestriction = new AudienceRestriction { Audience = new List<string>() { "http://localhost:60879/" } };
            audienceRestrictions.Add(audienceRestriction);

            authnRequest.Conditions = new Conditions { Items = audienceRestrictions };
            var requestBuilder = new AuthnRequestBuilder();
            var serialiser = new XMLSerialiser();
            
            serialiser.XmlNamespaces.Add("samlp", Saml20Constants.Protocol);
            serialiser.XmlNamespaces.Add("saml", Saml20Constants.Assertion);
            
            using (var ms = new MemoryStream())
            {
                serialiser.Serialize(ms, new[] { authnRequest });
                ms.Position = 0;
                var streamReader = new StreamReader(ms);
                var xmlString = streamReader.ReadToEnd();
                ms.Position = 0;
                var encoded = requestBuilder.DeflateEncode(xmlString);
                var result = authnRequest.Destination + "?SAMLRequest=" + Uri.EscapeDataString(encoded);
                return new Uri(result);
            }
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