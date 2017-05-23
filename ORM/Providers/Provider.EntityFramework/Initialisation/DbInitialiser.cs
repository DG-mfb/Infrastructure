using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.EntityFramework.Initialisation
{
	internal partial class DbInitialiser : CreateDatabaseIfNotExists<DBContext>
	{
		/// <summary>
		/// Pre-populate any data here. EF will save it automatically. no need to call SaveChanges();
		/// </summary>
		/// <param name="context"></param>
		protected override void Seed(DBContext context)
		{
			//var client = ConfigurationManager.AppSettings["client"];
			//if (String.IsNullOrWhiteSpace(client))
			//	throw new ArgumentException("client");

			//var seeders = ClientSeeder.GetSeedersFor(client);

			//foreach (var seeder in seeders.OrderBy(x => x.SeedingOrder))
			//{
			//	seeder.Seed(context);
			//}
		}
	}
}