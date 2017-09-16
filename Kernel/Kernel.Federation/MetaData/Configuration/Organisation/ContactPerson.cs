﻿using System.Collections.Generic;
using Kernel.Federation.MetaData.Configuration.Miscellaneous;

namespace Kernel.Federation.MetaData.Configuration.Organisation
{
    public class ContactPerson : MetadataConfigurationEntry
    {
        public ContactType ContactType { get; set; }
        public string ForeName { get; set; }
        public string SurName { get; set; }
        public ICollection<OtherName> OtherNames { get; }
        public ICollection<string> Emails { get; }
        public ICollection<string> PhoneNumbers { get; }
    }
}