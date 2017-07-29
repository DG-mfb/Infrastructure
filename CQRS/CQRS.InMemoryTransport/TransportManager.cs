using System.Threading.Tasks;
using Kernel.CQRS.Transport;

namespace CQRS.InMemoryTransport
{
    internal class TransportManager : ITransportManager
    {
        private readonly InMemoryTransport _transport;

        public TransportManager(InMemoryTransport transport)
        {
            this._transport = transport;
            transport.RegisterManager(this);
        }

        public Task<bool> EnqueueMessage(byte[] message)
        {
            var result = this._transport.Enque(message);
            return Task.FromResult(result);
        }

        public Task Initialise()
        {
            return this._transport.Initialise();
        }

        public Task Start()
        {
            return this._transport.Start();
        }

        public Task Stop()
        {
            return this._transport.Stop();
        }
    }
}