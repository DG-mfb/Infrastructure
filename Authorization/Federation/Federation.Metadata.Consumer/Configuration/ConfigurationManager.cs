using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kernel.Extensions;
using Kernel.Federation.RelyingParty;

namespace Federation.Metadata.RelyingParty.Configuration
{
    public class ConfigurationManager<T> : IConfigurationManager<T> where T : class
    {
        private static ConcurrentDictionary<RelyingPartyContext, T> _configurationCache = new ConcurrentDictionary<RelyingPartyContext, T>();
        private readonly SemaphoreSlim _refreshLock;
        
        private readonly IConfigurationRetriever<T> _configRetriever;
        private readonly IRelyingPartyContextBuilder _relyingPartyContextBuilder;
        //private T _currentConfiguration;
        
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
            
            var configuration = ConfigurationManager<T>._configurationCache.GetOrAdd(context, c =>
            {
                var task = Task.Factory.StartNew<Task<T>>(async() => await this.GetConfiguration(c, cancel));
                return task.Result.Result;
            });
            return configuration;
        }

        public void RequestRefresh(string relyingPartyId)
        {
            var contextEntry = ConfigurationManager<T>._configurationCache.FirstOrDefault(x => x.Key.RelyingPartyId.Equals(relyingPartyId, StringComparison.OrdinalIgnoreCase));
            if (contextEntry.Key == null)
                return;
            var context = contextEntry.Key;   
            var utcNow = DateTimeOffset.UtcNow;
            if (!(utcNow >= DataTimeExtensions.Add(context.LastRefresh.UtcDateTime, context.RefreshInterval)))
                return;
            context.SyncAfter = utcNow;
        }

        private async Task<T> GetConfiguration(RelyingPartyContext c, CancellationToken cancel)
        {
            var now = DateTimeOffset.UtcNow;
           
            T currentConfiguration = null;
            await this._refreshLock.WaitAsync(cancel);
            try
            {

                int num = 0;
                if (num == 1 || c.SyncAfter <= now)
                {
                    try
                    {
                        ConfigurationManager<T> configurationManager = this;
                        
                        T obj = await this._configRetriever.GetAsync(c.MetadataAddress, CancellationToken.None).ConfigureAwait(false);
                        currentConfiguration = obj;
                        
                        configurationManager = null;
                        obj = default(T);

                        c.LastRefresh = now;
                        c.SyncAfter = DataTimeExtensions.Add(now.UtcDateTime, c.AutomaticRefreshInterval);
                    }
                    catch (Exception ex)
                    {
                        c.SyncAfter = DataTimeExtensions.Add(now.UtcDateTime, c.AutomaticRefreshInterval < c.RefreshInterval ? c.AutomaticRefreshInterval : c.RefreshInterval);
                        throw new InvalidOperationException(String.Format("IDX10803: Unable to obtain configuration from: '{0}'.", (c.MetadataAddress ?? "null")), ex);
                    }
                }
                if (currentConfiguration != null)
                    return currentConfiguration;

                throw new InvalidOperationException(String.Format("IDX10803: Unable to obtain configuration from: '{0}'.", (c.MetadataAddress ?? "null")));
            }
            finally
            {
                this._refreshLock.Release();
            }
        }
    }
}