using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kernel.Data;

namespace ORMMetadataContextProvider.Models
{
    public class SSODescriptorSettings : RoleDescriptorSettings
    {
        public SSODescriptorSettings()
        {
            this.LogoutServices = new List<EndPointSetting>();
        }
        public virtual ICollection<EndPointSetting> LogoutServices { get; }
        
    }
}