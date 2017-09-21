using Kernel.Data.ORM;
using ORMMetadataContextProvider.Models;

namespace ORMMetadataContextProvider.Seeders
{
    class RelyingPartySeeder : ISeeder
    {
        public string ClientIdentifier { get; }

        public byte SeedingOrder { get { return 0; } }

        public void Seed(IDbContext context)
        {
            var relyingParty = new RelyingPartySettings
            {
                RefreshInterval = 30,
                AutoRefreshInterval = 1000,
                MetadataPath = "https://shibboleth.imperial.ac.uk/idp/shibboleth",
                MetadataLocation = "HTTP",
                RelyingPartyId = "imperial.ac.uk"
            };
            context.Add<RelyingPartySettings>(relyingParty);  
        }
    }
}