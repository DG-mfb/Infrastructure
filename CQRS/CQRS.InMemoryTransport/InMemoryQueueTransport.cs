using System.Collections.Concurrent;
using System.Threading.Tasks;
using Kernel.CQRS.Transport;

namespace CQRS.InMemoryTransport
{
    internal class InMemoryQueueTransport : ITransport
    {
        private ConcurrentQueue<byte[]> _queue;
        private bool _isStarted;
        private ITransportManager _manager;

        public bool IsEmpty
        {
            get
            {
                return this._queue.IsEmpty;
            }
        }

        public ITransportManager Manager
        {
            get
            {
                return this._manager;
            }
        }

        public InMemoryQueueTransport()
        {
            this._queue = new ConcurrentQueue<byte[]>();
        }
        internal void RegisterManager(ITransportManager manager)
        {
            this._manager = manager;
        }

        public Task Initialise()
        {
            this._queue = new ConcurrentQueue<byte[]>();
            return Task.CompletedTask;
        }

        public Task Start()
        {
            this._isStarted = true;
            return Task.CompletedTask;
        }

        public Task Stop()
        {
            this._isStarted = false;
            return Task.CompletedTask;
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
    }
}