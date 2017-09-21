using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kernel.Data;

namespace ORMMetadataContextProvider.Models
{
    public class SPMetadataSettings: BaseModel
    {
        public string AssertionLocation { get; set; }
    }
}
