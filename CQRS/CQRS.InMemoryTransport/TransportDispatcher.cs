using System.IO;
using System.Threading.Tasks;
using CQRS.Infrastructure.Messaging;
using Kernel.CQRS.Messaging;
using Kernel.CQRS.Transport;

namespace CQRS.InMemoryTransport
{
    internal class TransportDispatcher : ITranspontDispatcher
    {
        private readonly IMessageSerialiser _serialiser;
        private readonly ITransportManager _transport;
        public TransportDispatcher(ITransportManager transport, IMessageSerialiser serialiser)
        {
            this._serialiser = serialiser;
            this._transport = transport;
        }

        public ITransportManager TransportManager
        {
            get
            {
                return this._transport;
            }
        }

        public async Task SendMessage<TMessage>(TMessage message) where TMessage : Message
        {
            using (var ms = new MemoryStream())
            {
                await this._serialiser.SerialiseAsync(ms, message);
                var serialsed = ms.ToArray();
                await this._transport.EnqueueMessage(serialsed);
            }
        }
    }
}