using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Federation.MetaData.Configuration.Cryptography
{
    public class KeyDescriptorConfiguration
    {
        public KeyUsage Use { get; set; }

        public Uri Algorithm { get; }
        public object KeyInfo { get; set; }
    }
}