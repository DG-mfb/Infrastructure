using System;
using System.Collections.Generic;

namespace Kernel.Federation.MetaData.Configuration.Organisation
{
    public class OrganisationConfiguration
    {
        public ICollection<LocalizedConfigurationEntry> Names { get; }
        public ContactConfiguration OrganisationContacts { get; set; }
        public ICollection<Uri> Urls { get; }
        public OrganisationConfiguration()
        {
            this.Urls = new List<Uri>();
            this.Names = new List<LocalizedConfigurationEntry>();
        }
    }
}