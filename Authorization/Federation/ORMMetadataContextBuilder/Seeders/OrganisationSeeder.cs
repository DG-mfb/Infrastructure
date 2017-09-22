using System.Globalization;
using Kernel.Data.ORM;
using ORMMetadataContextProvider.Models;

namespace ORMMetadataContextProvider.Seeders
{
    internal class OrganisationSeeder : Seeder
    {
        public override void Seed(IDbContext context)
        {
            var organisation = new OrganisationSettings();
            organisation.Names.Add(new LocalisedName { Language = CultureInfo.CurrentCulture.Name, Name = "Apira LTD" });
            organisation.Urls.Add(new LocalisedName { Language = CultureInfo.CurrentCulture.Name, Name = "https://apira.co.uk/" });
            Seeder._cache.Add(Seeder.Organisation, organisation);
            context.Add<OrganisationSettings>(organisation);  
        }
    }
}