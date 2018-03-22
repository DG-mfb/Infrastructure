using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Kernel.CQRS.Transport;
using Kernel.Logging;

namespace CQRS.InMemoryTransport
{
    internal class InMemoryQueueTransport : ITransport
    {
        private Timer _timer;
        private ConcurrentQueue<byte[]> _queue;
        private bool _isStarted;
        private readonly ILogProvider _logger;
        private volatile AutoResetEvent autoResetEvent = new AutoResetEvent(true);
        private readonly ITransportConfiguration _configuration;
        private int _activeWorkers = 0;
        public bool IsEmpty
        {
            get
            {
                return this._queue.IsEmpty;
            }
        }
        
        public bool IsTransactional { get { return false; } }

        public int PendingMessages { get { return this._queue.Count; } }

        public ITransportConfiguration Configuration
        {
            get
            {
                return this._configuration;
            }
        }

        public InMemoryQueueTransport(ILogProvider logger, Func<ITransportConfiguration> configurationFactory)
        {
            this._queue = new ConcurrentQueue<byte[]>();
            this._logger = logger;
            this._configuration = configurationFactory();
        }
        
        public Task Initialise()
        {
            this._queue = new ConcurrentQueue<byte[]>();
            return Task.CompletedTask;
        }

        public Task Start()
        {
            if (this._configuration.ConsumerPeriod != Timeout.InfiniteTimeSpan)
                this._timer = new Timer(new TimerCallback(o => this.Consume(this._configuration)), this._configuration, TimeSpan.FromMilliseconds(0), this._configuration.ConsumerPeriod);
            this._isStarted = true;
            return Task.CompletedTask;
        }

        public async Task Stop()
        {
            this._timer.Dispose();
            this._isStarted = false;
            this.Consume(this._configuration);
        }

        public Task<bool> Send(byte[] message)
        {
            this.Enque(message);
            return Task.FromResult(true);
        }

        public bool Enque(byte[] message)
        {
            if (!this._isStarted)
                return false;

            this._queue.Enqueue(message);
            return true;
        }

        public bool TryDequeue(out byte[] message)
        {
            return this._queue.TryDequeue(out message);
        }

        public Task<IEnumerable<byte[]>> ReadAllMessages()
        {
            var current = Interlocked.Exchange(ref this._queue, new ConcurrentQueue<byte[]>());
            var messages = current.ToArray();

            return Task.FromResult<IEnumerable<byte[]>>(messages);
        }

        public Task CopyMessages(byte[][] destination)
        {
            this._queue.CopyTo(destination, 0);
            return Task.CompletedTask;
        }

        public void Consume(object configuration)
        {
            var transportConfiguration = configuration as ITransportConfiguration;
            if (transportConfiguration == null)
                throw new InvalidOperationException(String.Format("Expected configuration of type: {0}", typeof(ITransportConfiguration)));

            var workers = 0;
            if (Interlocked.Exchange(ref workers, this._activeWorkers) > transportConfiguration.MaxDegreeOfParallelism)
                return;
           
            if (this._queue.IsEmpty)
            {
                return;
            }
            this.autoResetEvent.WaitOne();
            var messageToConsume = this._queue.Count;
            
            for (var i = 0; i < this._configuration.MaxDegreeOfParallelism; i++)
            {
                var task = Task.Factory.StartNew(async () =>
                {
                    try
                    {
                        Interlocked.Increment(ref this._activeWorkers);
                        byte[] message;
                        while (messageToConsume > 0 && this.TryDequeue(out message))
                        {
                            Interlocked.Decrement(ref messageToConsume);
                            await MessageConsumedInternal(message);
                        }
                    }
                    catch (Exception ex)
                    {
                        Exception inner;
                        this._logger.TryCommitException(ex, out inner);
                    }
                })
                .ContinueWith(t => Interlocked.Decrement(ref this._activeWorkers));
            }
            this.autoResetEvent.Set();
        }

        private Task MessageConsumedInternal(byte[] message)
        {
            var result = Parallel.ForEach(this._configuration.Listeners, new ParallelOptions { MaxDegreeOfParallelism = this._configuration.MaxDegreeOfParallelism }, async s =>
            {
                try
                {
                    await s.ReceiveMessage(message);
                }
                catch (Exception e)
                {
                    Exception inner;
                    this._logger.TryCommitException(e, out inner);
                }
            });
            return Task.CompletedTask;
        }
    }
}