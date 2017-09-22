using System.Linq;
using Kernel.Data.ORM;
using Kernel.Federation.MetaData.Configuration;
using ORMMetadataContextProvider.Models;

namespace ORMMetadataContextProvider
{
    public class MetadataContextBuilder : IMetadataContextBuilder
    {
        private readonly IDbContext _dbContext;
        public MetadataContextBuilder(IDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public MetadataContext BuildContext()
        {
            var foo = this._dbContext.Set<EntityDescriptorSettings>()
                .First();

            return new MetadataContext();
        }

        public void Dispose()
        {
            this._dbContext.Dispose();
        }
    }
}