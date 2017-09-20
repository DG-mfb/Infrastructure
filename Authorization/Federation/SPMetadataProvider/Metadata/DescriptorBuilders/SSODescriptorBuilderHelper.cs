using System;
using System.Globalization;
using System.IdentityModel.Metadata;
using System.Linq;
using Kernel.Federation.MetaData.Configuration.RoleDescriptors;

namespace WsFederationMetadataProvider.Metadata.DescriptorBuilders
{
    internal class SSODescriptorBuilderHelper
    {
        internal static void BuildOrganisation(RoleDescriptor roleDescriptor, RoleDescriptorConfiguration roleDescriptorConfiguration)
        {
            if (roleDescriptor == null)
                throw new ArgumentNullException("roleDescriptor");
            if (roleDescriptorConfiguration == null)
                throw new ArgumentNullException("roleDescriptorConfiguration");

            var organisationConfigration = roleDescriptorConfiguration.Organisation;
            if (organisationConfigration == null)
                return;
            roleDescriptor.Organization = new Organization();
            organisationConfigration.Names.Aggregate(roleDescriptor.Organization, (o, next) =>
            {
                o.Names.Add(new LocalizedName(next.Name, next.Language));
                o.DisplayNames.Add(new LocalizedName(next.DisplayName, next.Language));
                return o;
            });
            organisationConfigration.Urls.Aggregate(roleDescriptor.Organization, (o, next) =>
            {
                o.Urls.Add(new LocalizedUri(next, CultureInfo.CurrentCulture));
                return o;
            });

            var contacts = roleDescriptorConfiguration.Organisation.OrganisationContacts.PersonContact;
            contacts.Aggregate(roleDescriptor.Contacts, (c, next) =>
            {
                ContactType contactType;
                if (!Enum.TryParse<ContactType>(next.ContactType.ToString(), out contactType))
                    throw new InvalidCastException(String.Format("No corespondenting value for Contact type: {0}.", next.ContactType));
                c.Add(new ContactPerson(contactType)
                {
                    Surname = next.SurName,
                    GivenName = next.ForeName
                });
                return c;
            });
        }
        internal static void BuildContacts(RoleDescriptor roleDescriptor, RoleDescriptorConfiguration roleDescriptorConfiguration)
        {
            if (roleDescriptor == null)
                throw new ArgumentNullException("roleDescriptor");
            if (roleDescriptorConfiguration == null)
                throw new ArgumentNullException("roleDescriptorConfiguration");
            
            var contacts = roleDescriptorConfiguration.Organisation.OrganisationContacts.PersonContact;
            contacts.Aggregate(roleDescriptor.Contacts, (c, next) =>
            {
                ContactType contactType;
                if (!Enum.TryParse<ContactType>(next.ContactType.ToString(), out contactType))
                    throw new InvalidCastException(String.Format("No corespondenting value for Contact type: {0}.", next.ContactType));
                c.Add(new ContactPerson(contactType)
                {
                    Surname = next.SurName,
                    GivenName = next.ForeName
                });
                return c;
            });
        }
    }
}