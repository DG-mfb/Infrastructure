﻿using System;
using System.Collections.Generic;
using System.Linq;
using Kernel.Data.ORM;
using ORMMetadataContextProvider.Models;

namespace ORMMetadataContextProvider.Seeders
{
    internal class EntityDescriptorSeeder : Seeder
    {
        public override byte SeedingOrder { get { return 2; } }

        public override void Seed(IDbContext context)
        {
            var descriptor = new EntityDescriptorSettings
            {
                CacheDuration = 100,
                ValidUntil = DateTimeOffset.Now.AddDays(90),
            };

            
            //sp descriptors
            var spDescriptors = Seeder._cache[Seeder.SPDescriptorsKey] as IEnumerable<SPDescriptorSettings>;
            spDescriptors.Aggregate(descriptor, (d, next) => { d.RoleDescriptors.Add(next); return d; });

            context.Add<EntityDescriptorSettings>(descriptor);
        }
    }
}