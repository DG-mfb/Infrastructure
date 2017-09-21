using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kernel.Data;

namespace ORMMetadataContextProvider.Models
{
    public class SPDescriptorSettings : SSODescriptorSettings
    {
        public SPDescriptorSettings()
        {
            this.AssertionServices = new List<EndPointSetting>();
        }
        public virtual ICollection<EndPointSetting> AssertionServices { get; }
        
    }
}