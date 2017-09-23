using System;

namespace Kernel.Federation.RelyingParty
{
    public class RelyingPartyContext
    {
        public static readonly TimeSpan DefaultAutomaticRefreshInterval = new TimeSpan(1, 0, 0, 0);
        public static readonly TimeSpan DefaultRefreshInterval = new TimeSpan(0, 0, 0, 30);
        public static readonly TimeSpan MinimumAutomaticRefreshInterval = new TimeSpan(0, 0, 5, 0);
        public static readonly TimeSpan MinimumRefreshInterval = new TimeSpan(0, 0, 0, 1);

        private TimeSpan _automaticRefreshInterval;
        private TimeSpan _refreshInterval;
        
        public string MetadataAddress { get; }

        public TimeSpan AutomaticRefreshInterval
        {
            get
            {
                return this._automaticRefreshInterval;
            }
            set
            {
                if (value < RelyingPartyContext.MinimumAutomaticRefreshInterval)
                    throw new ArgumentOutOfRangeException("value", String.Format("IDX10107: When setting AutomaticRefreshInterval, the value must be greater than MinimumAutomaticRefreshInterval: '{0}'. value: '{1}'.", RelyingPartyContext.MinimumAutomaticRefreshInterval, value));
                this._automaticRefreshInterval = value;
            }
        }
        
        public TimeSpan RefreshInterval
        {
            get
            {
                return this._refreshInterval;
            }
            set
            {
                if (value < RelyingPartyContext.MinimumRefreshInterval)
                    throw new ArgumentOutOfRangeException("value", String.Format("IDX10106: When setting RefreshInterval, the value must be greater than MinimumRefreshInterval: '{0}'. value: '{1}'.", RelyingPartyContext.MinimumRefreshInterval, value));
                this._refreshInterval = value;
            }
        }
        public RelyingPartyContext(string metadataAddress)
        {
            if (String.IsNullOrWhiteSpace(metadataAddress))
                throw new ArgumentNullException("metadataContext");
            this.MetadataAddress = metadataAddress;
            this.AutomaticRefreshInterval = RelyingPartyContext.DefaultAutomaticRefreshInterval;
            this.RefreshInterval = RelyingPartyContext.DefaultRefreshInterval;
        }
    }
}