﻿using System.Collections.Generic;
using Kernel.Data;
using Kernel.Federation.MetaData.Configuration.Cryptography;

namespace ORMMetadataContextProvider.Models
{
    public class Certificate : BaseModel
    {
        public Certificate()
        {
            this.StoreSearchCriteria = new List<StoreSearchCriterion>();
        }
        public string CertificatePath { get; }
        public string CertificatePKPath { get; set; }
        public string CertificatePfxPath { get; set; }
        public string Password { get; set; }
        public string CetrificateStore { get; set; }
        public KeyUsage Use { get; set; }
        public bool IsDefault { get; set; }
        public virtual ICollection<StoreSearchCriterion> StoreSearchCriteria { get; }
        public virtual ICollection<SigningCredential> SigningCredentials { get; set; }
    }
}