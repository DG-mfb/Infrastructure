using System;
using System.Xml;
using Microsoft.IdentityModel.Protocols.WSFederation.Metadata;

namespace WsFederationMetadataProvider.Extensions
{
    public static class EntityDescriptorExtensions
    {
        public static XmlElement ToXml(this EntityDescriptor descriptor)
        {
            var xmlDocument = new XmlDocument()
            {
                PreserveWhitespace = true,
                XmlResolver = (XmlResolver)null
            };

            XmlElement element = xmlDocument.CreateElement("md", Saml2MetadataConstants.Elements.EntitiesDescriptor, "urn:oasis:names:tc:SAML:2.0:metadata");
            if (descriptor.EntityId != null)
                element.SetAttribute(Saml2MetadataConstants.Attributes.EntityId, descriptor.EntityId.ToString());
            //if (this.validUntil != DateTime.MaxValue)
            //    element.SetAttribute("validUntil", SAML.ToDateTimeString(this.validUntil));
            //if (this.cacheDuration != null)
            //    element.SetAttribute("cacheDuration", this.cacheDuration.ToString());
            //if (!string.IsNullOrEmpty(this.id))
            //    element.SetAttribute("ID", this.id);
            //if (this.signature != null)
            //    element.AppendChild(xmlDocument.ImportNode((XmlNode)this.signature, true));
            //if (this.extensions != null)
            //    element.AppendChild((XmlNode)this.extensions.ToXml(xmlDocument));
            //foreach (RoleDescriptor roleDescriptor in (IEnumerable<RoleDescriptor>)this.roleDescriptors)
            //    element.AppendChild((XmlNode)roleDescriptor.ToXml(xmlDocument));
            //foreach (IDPSSODescriptor idpSsoDescriptor in (IEnumerable<IDPSSODescriptor>)this.idpSSODescriptors)
            //    element.AppendChild((XmlNode)idpSsoDescriptor.ToXml(xmlDocument));
            //foreach (SPSSODescriptor spSsoDescriptor in (IEnumerable<SPSSODescriptor>)this.spSSODescriptors)
            //    element.AppendChild((XmlNode)spSsoDescriptor.ToXml(xmlDocument));
            //foreach (AuthnAuthorityDescriptor authorityDescriptor in (IEnumerable<AuthnAuthorityDescriptor>)this.authnAuthorityDescriptors)
            //    element.AppendChild((XmlNode)authorityDescriptor.ToXml(xmlDocument));
            //foreach (AttributeAuthorityDescriptor authorityDescriptor in (IEnumerable<AttributeAuthorityDescriptor>)this.attributeAuthorityDescriptors)
            //    element.AppendChild((XmlNode)authorityDescriptor.ToXml(xmlDocument));
            //foreach (PDPDescriptor pdpDescriptor in (IEnumerable<PDPDescriptor>)this.pdpDescriptors)
            //    element.AppendChild((XmlNode)pdpDescriptor.ToXml(xmlDocument));
            //if (this.affiliationDescriptor != null)
            //    element.AppendChild((XmlNode)this.affiliationDescriptor.ToXml(xmlDocument));
            //if (this.organization != null)
            //    element.AppendChild((XmlNode)this.organization.ToXml(xmlDocument));
            //foreach (ContactPerson contactPerson in (IEnumerable<ContactPerson>)this.contactPeople)
            //    element.AppendChild((XmlNode)contactPerson.ToXml(xmlDocument));
            //foreach (AdditionalMetadataLocation metadataLocation in (IEnumerable<AdditionalMetadataLocation>)this.additionalMetadataLocations)
            //    element.AppendChild((XmlNode)metadataLocation.ToXml(xmlDocument));
            
            return element;
        }
    }
}