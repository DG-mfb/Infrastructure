using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kernel.Data.ORM;
using ORMMetadataContextProvider.Models;

namespace ORMMetadataContextProvider.Seeders
{
    class ProtocolSeeder : ISeeder
    {
        public string ClientIdentifier { get; }

        public byte SeedingOrder { get { return 0; } }

        public void Seed(IDbContext context)
        {
            var protocol = new Protocol { Uri = "urn:oasis:names:tc:SAML:2.0:protocol" };
            context.Add<Protocol>(protocol);
        }
    }
}
