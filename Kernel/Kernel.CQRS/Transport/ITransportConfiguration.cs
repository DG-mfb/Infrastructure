using System;
using System.Collections.Generic;

namespace Kernel.CQRS.Transport
{
    public interface ITransportConfiguration
    {
        int MaxDegreeOfParallelism { get; set; }
        TimeSpan ConsumerPeriod { get; set; }
        ICollection<IMessageListener> Listeners { get; }
    }
}