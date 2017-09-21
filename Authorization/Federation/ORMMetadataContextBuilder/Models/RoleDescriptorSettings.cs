﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kernel.Data;

namespace ORMMetadataContextProvider.Models
{
    public class RoleDescriptorSettings : BaseModel
    {
        public RoleDescriptorSettings()
        {
            this.Protocols = new List<Protocol>();
            this.Certificates = new List<Certificate>();
        }
        public virtual ICollection<Protocol> Protocols { get; }
        public virtual ICollection<Certificate> Certificates { get; }
    }
}