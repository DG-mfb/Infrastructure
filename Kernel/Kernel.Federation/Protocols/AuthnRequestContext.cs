using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kernel.Federation.MetaData;

namespace Kernel.Federation.Protocols
{
    public class AuthnRequestContext
    {
        public AuthnRequestContext(IMetadataConfiguration configuration, Uri destination)
        {
            this.Destination = destination;
            this.Configuration = configuration;
        }
        public Uri Destination { get; private set; }
        public IMetadataConfiguration Configuration { get; private set; }
    }
}
