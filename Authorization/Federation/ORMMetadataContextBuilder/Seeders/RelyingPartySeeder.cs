﻿using Kernel.Data.ORM;
using ORMMetadataContextProvider.Models;

namespace ORMMetadataContextProvider.Seeders
{
    internal class RelyingPartySeeder : Seeder
    {
        public override void Seed(IDbContext context)
        {
            var relyingParty = new RelyingPartySettings
            {
                RefreshInterval = 30,
                AutoRefreshInterval = 1000,
                MetadataPath = "https://shibboleth.imperial.ac.uk/idp/shibboleth",
                MetadataLocation = "HTTP",
                RelyingPartyId = "imperial.ac.uk"
            };
            var testRelyingParty = new RelyingPartySettings
            {
                RefreshInterval = 30,
                AutoRefreshInterval = 1000,
                MetadataPath = "https://www.testshib.org/metadata/testshib-providers.xml",
                MetadataLocation = "HTTP",
                RelyingPartyId = "testShib"
            };
            context.Add<RelyingPartySettings>(relyingParty);
            context.Add<RelyingPartySettings>(testRelyingParty);
        }
    }
}