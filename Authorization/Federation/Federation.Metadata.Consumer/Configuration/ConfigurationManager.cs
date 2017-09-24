using System;
using System.Threading;
using System.Threading.Tasks;
using Kernel.Extensions;
using Kernel.Federation.RelyingParty;

namespace Federation.Metadata.RelyingParty.Configuration
{
    public class ConfigurationManager<T> : IConfigurationManager<T> where T : class
    {
        private readonly SemaphoreSlim _refreshLock;
        private readonly IConfigurationRetriever<T> _configRetriever;
        private readonly IRelyingPartyContextBuilder _relyingPartyContextBuilder;
        
        public ConfigurationManager(IRelyingPartyContextBuilder relyingPartyContextBuilder, IConfigurationRetriever<T> configRetriever)
        {
            if (relyingPartyContextBuilder == null)
                throw new ArgumentNullException("context");

            if (configRetriever == null)
                throw new ArgumentNullException("configRetriever");
            
            this._relyingPartyContextBuilder = relyingPartyContextBuilder;
            this._configRetriever = configRetriever;
            this._refreshLock = new SemaphoreSlim(1);
        }

        public async Task<T> GetConfigurationAsync(string relyingPrtyId)
        {
            T configuration = await this.GetConfigurationAsync(relyingPrtyId, CancellationToken.None)
                .ConfigureAwait(false);
            return configuration;
        }

        public async Task<T> GetConfigurationAsync(string relyingPrtyId, CancellationToken cancel)
        {
            var context = this._relyingPartyContextBuilder.BuildRelyingPartyContext(relyingPrtyId);

            var configuration = await this.GetConfiguration(context, cancel);
            return configuration;
        }

        public void RequestRefresh(string relyingPartyId)
        {
            var context = this._relyingPartyContextBuilder.BuildRelyingPartyContext(relyingPartyId);
            var utcNow = DateTimeOffset.UtcNow;
            if (!(utcNow >= DataTimeExtensions.Add(context.LastRefresh.UtcDateTime, context.RefreshInterval)))
                return;
            context.SyncAfter = utcNow;
        }

        private async Task<T> GetConfiguration(RelyingPartyContext context, CancellationToken cancel)
        {
            var now = DateTimeOffset.UtcNow;
           
            T currentConfiguration = null;
            await this._refreshLock.WaitAsync(cancel);
            try
            {

                int num = 0;
                if (num == 1 || context.SyncAfter <= now)
                {
                    try
                    {
                        ConfigurationManager<T> configurationManager = this;
                        
                        T obj = await this._configRetriever.GetAsync(context.MetadataAddress, CancellationToken.None).ConfigureAwait(false);
                        currentConfiguration = obj;
                        
                        configurationManager = null;
                        obj = default(T);

                        context.LastRefresh = now;
                        context.SyncAfter = DataTimeExtensions.Add(now.UtcDateTime, context.AutomaticRefreshInterval);
                    }
                    catch (Exception ex)
                    {
                        context.SyncAfter = DataTimeExtensions.Add(now.UtcDateTime, context.AutomaticRefreshInterval < context.RefreshInterval ? context.AutomaticRefreshInterval : context.RefreshInterval);
                        throw new InvalidOperationException(String.Format("IDX10803: Unable to obtain configuration from: '{0}'.", (context.MetadataAddress ?? "null")), ex);
                    }
                }
                if (currentConfiguration != null)
                    return currentConfiguration;

                throw new InvalidOperationException(String.Format("IDX10803: Unable to obtain configuration from: '{0}'.", (context.MetadataAddress ?? "null")));
            }
            finally
            {
                this._refreshLock.Release();
            }
        }
    }
}