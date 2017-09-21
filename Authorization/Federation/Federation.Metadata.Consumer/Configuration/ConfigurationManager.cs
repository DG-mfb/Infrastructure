using System;
using System.Threading;
using System.Threading.Tasks;
using Kernel.Extensions;
using Kernel.Federation.MetadataConsumer;

namespace Federation.Metadata.Consumer.Configuration
{
    public class ConfigurationManager<T> : IConfigurationManager<T> where T : class
    {
        private DateTimeOffset _syncAfter = DateTimeOffset.MinValue;
        private DateTimeOffset _lastRefresh = DateTimeOffset.MinValue;
       
        private readonly SemaphoreSlim _refreshLock;
        
        private readonly IConfigurationRetriever<T> _configRetriever;
        private readonly MetadataConsumerContext _context;
        private T _currentConfiguration;


        public TimeSpan AutomaticRefreshInterval
        {
            get
            {
                return this._context.AutomaticRefreshInterval;
            }
        }

        public DateTimeOffset LastRefresh { get { return this._lastRefresh; } }

        public TimeSpan RefreshInterval
        {
            get
            {
                return this._context.RefreshInterval;
            }
        }
        
        public ConfigurationManager(MetadataConsumerContext context, IConfigurationRetriever<T> configRetriever)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (configRetriever == null)
                throw new ArgumentNullException("configRetriever");
            
            this._context = context;
            this._configRetriever = configRetriever;
            this._refreshLock = new SemaphoreSlim(1);
        }

        public async Task<T> GetConfigurationAsync()
        {
            T configuration = await this.GetConfigurationAsync(CancellationToken.None)
                .ConfigureAwait(false);
            return configuration;
        }

        public async Task<T> GetConfigurationAsync(CancellationToken cancel)
        {
            var now = DateTimeOffset.UtcNow;
            if (this._currentConfiguration != null && this._syncAfter > now)
                return this._currentConfiguration;

            await this._refreshLock.WaitAsync(cancel);
            try
            {
                int num = 0;
                if (num == 1 || this._syncAfter <= now)
                {
                    try
                    {
                        ConfigurationManager<T> configurationManager = this;
                        T currentConfiguration = configurationManager._currentConfiguration;
                        T obj = await this._configRetriever.GetAsync(this._context.MetadataAddress, CancellationToken.None).ConfigureAwait(false);
                        configurationManager._currentConfiguration = obj;
                        configurationManager = null;
                        obj = default(T);
                        
                        this._lastRefresh = now;
                        this._syncAfter = DataTimeExtensions.Add(now.UtcDateTime, this._context.AutomaticRefreshInterval);
                    }
                    catch (Exception ex)
                    {
                        this._syncAfter = DataTimeExtensions.Add(now.UtcDateTime, this._context.AutomaticRefreshInterval < this._context.RefreshInterval ? this._context.AutomaticRefreshInterval : this._context.RefreshInterval);
                        throw new InvalidOperationException(String.Format("IDX10803: Unable to obtain configuration from: '{0}'.", (this._context.MetadataAddress ?? "null")), ex);
                    }
                }
                if (this._currentConfiguration != null)
                    return this._currentConfiguration;

                throw new InvalidOperationException(String.Format("IDX10803: Unable to obtain configuration from: '{0}'.", (this._context.MetadataAddress ?? "null")));
            }
            finally
            {
                this._refreshLock.Release();
            }
        }

        public void RequestRefresh()
        {
            var utcNow = DateTimeOffset.UtcNow;
            if (!(utcNow >= DataTimeExtensions.Add(this._lastRefresh.UtcDateTime, this.RefreshInterval)))
                return;
            this._syncAfter = utcNow;
        }
    }
}