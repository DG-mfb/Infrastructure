using System;
using System.Linq;
using Kernel.Data.ORM;
using Kernel.Federation.RelyingParty;
using ORMMetadataContextProvider.Models;

namespace ORMMetadataContextProvider.RelyingParty
{
    internal class RelyingPartyContextBuilder : IRelyingPartyContextBuilder
    {
        private readonly IDbContext _dbContext;
        public RelyingPartyContextBuilder(IDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public RelyingPartyContext BuildRelyingPartyContext(string relyingPartyId)
        {
            var relyingPartyContext = this._dbContext.Set<RelyingPartySettings>()
                .FirstOrDefault(x => x.RelyingPartyId == relyingPartyId);

            var context = new RelyingPartyContext(relyingPartyId, relyingPartyContext.MetadataPath);
            context.RefreshInterval = TimeSpan.FromSeconds(relyingPartyContext.RefreshInterval);
            context.AutomaticRefreshInterval = TimeSpan.FromDays(relyingPartyContext.AutoRefreshInterval);
            return context;
        }

        public void Dispose()
        {
            if(this._dbContext != null)
                this._dbContext.Dispose();
        }
    }
}