using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kernel.CQRS.Transport;

namespace CQRS.InMemoryTransport
{
    internal class InMemoryTransport
    {
        private readonly ITransportManager _manager;
        private readonly ConcurrentQueue<byte[]> _queue;
        public bool IsEmpty
        {
            get
            {
                return this._queue.IsEmpty;
            }
        }

        public InMemoryTransport(ITransportManager manager)
        {
            this._manager = manager;
        }

        public void Enque(byte[] message)
        {
            this._queue.Enqueue(message);
        }

        public bool TryDequeue(out byte[] message)
        {
            return this._queue.TryDequeue(out message);   
        }
    }
}
