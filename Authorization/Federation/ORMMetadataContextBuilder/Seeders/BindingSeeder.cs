using Kernel.Data.ORM;
using ORMMetadataContextProvider.Models;

namespace ORMMetadataContextProvider.Seeders
{
    class BindingSeeder : ISeeder
    {
        public string ClientIdentifier { get; }

        public byte SeedingOrder { get { return 0; } }

        public void Seed(IDbContext context)
        {
            var redirectBinding = new Binding { Uri = "urn:oasis:names:tc:SAML:2.0:bindings:HTTP-Redirect", Name = "HTTP-Redirect" };
            context.Add<Binding>(redirectBinding);
            var postBinding = new Binding { Uri = "urn:oasis:names:tc:SAML:2.0:bindings:HTTP-POST", Name = "HTTP-POST" };
            context.Add<Binding>(postBinding);
        }
    }
}