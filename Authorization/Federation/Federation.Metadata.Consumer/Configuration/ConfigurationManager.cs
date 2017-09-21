﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Kernel.Extensions;
using Kernel.Federation.MetadataConsumer;

namespace Federation.Metadata.Consumer.Configuration
{
    public class ConfigurationManager<T> : IConfigurationManager<T> where T : class
    {
        private TimeSpan _automaticRefreshInterval = ConfigurationManager<T>.DefaultAutomaticRefreshInterval;
        private TimeSpan _refreshInterval = ConfigurationManager<T>.DefaultRefreshInterval;
        private DateTimeOffset _syncAfter = DateTimeOffset.MinValue;
        private DateTimeOffset _lastRefresh = DateTimeOffset.MinValue;
        public static readonly TimeSpan DefaultAutomaticRefreshInterval = new TimeSpan(1, 0, 0, 0);
        public static readonly TimeSpan DefaultRefreshInterval = new TimeSpan(0, 0, 0, 30);
        public static readonly TimeSpan MinimumAutomaticRefreshInterval = new TimeSpan(0, 0, 5, 0);
        public static readonly TimeSpan MinimumRefreshInterval = new TimeSpan(0, 0, 0, 1);
        private readonly SemaphoreSlim _refreshLock;
        private readonly string _metadataAddress;
        private readonly IConfigurationRetriever<T> _configRetriever;
        private T _currentConfiguration;

        public TimeSpan AutomaticRefreshInterval
        {
            get
            {
                return this._automaticRefreshInterval;
            }
            set
            {
                if (value < ConfigurationManager<T>.MinimumAutomaticRefreshInterval)
                    throw new ArgumentOutOfRangeException("value", String.Format("IDX10107: When setting AutomaticRefreshInterval, the value must be greater than MinimumAutomaticRefreshInterval: '{0}'. value: '{1}'.", (object)ConfigurationManager<T>.MinimumAutomaticRefreshInterval, (object)value));
                this._automaticRefreshInterval = value;
            }
        }

        public DateTimeOffset LastRefresh { get { return this._lastRefresh; } }

        public TimeSpan RefreshInterval
        {
            get
            {
                return this._refreshInterval;
            }
            set
            {
                if (value < ConfigurationManager<T>.MinimumRefreshInterval)
                    throw new ArgumentOutOfRangeException("value", String.Format("IDX10106: When setting RefreshInterval, the value must be greater than MinimumRefreshInterval: '{0}'. value: '{1}'.", (object)ConfigurationManager<T>.MinimumRefreshInterval, (object)value));
                this._refreshInterval = value;
            }
        }
        
        public ConfigurationManager(string metadataAddress, IConfigurationRetriever<T> configRetriever)
        {
            if (string.IsNullOrWhiteSpace(metadataAddress))
                throw new ArgumentNullException("metadataAddress");
            if (configRetriever == null)
                throw new ArgumentNullException("configRetriever");
            
            this._metadataAddress = metadataAddress;
            this._configRetriever = configRetriever;
            this._refreshLock = new SemaphoreSlim(1);
        }

        public async Task<T> GetConfigurationAsync()
        {
            T configuration = await this.GetConfigurationAsync(CancellationToken.None).ConfigureAwait(false);
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
                        T obj = await this._configRetriever.GetAsync(this._metadataAddress, CancellationToken.None).ConfigureAwait(false);
                        configurationManager._currentConfiguration = obj;
                        configurationManager = null;
                        obj = default(T);
                        
                        this._lastRefresh = now;
                        this._syncAfter = DataTimeExtensions.Add(now.UtcDateTime, this._automaticRefreshInterval);
                    }
                    catch (Exception ex)
                    {
                        this._syncAfter = DataTimeExtensions.Add(now.UtcDateTime, this._automaticRefreshInterval < this._refreshInterval ? this._automaticRefreshInterval : this._refreshInterval);
                        throw new InvalidOperationException(String.Format("IDX10803: Unable to obtain configuration from: '{0}'.", (this._metadataAddress ?? "null")), ex);
                    }
                }
                if (this._currentConfiguration != null)
                    return this._currentConfiguration;

                throw new InvalidOperationException(String.Format("IDX10803: Unable to obtain configuration from: '{0}'.", (object)(this._metadataAddress ?? "null")));
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