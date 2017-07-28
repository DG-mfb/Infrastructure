using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kernel.CQRS.Messaging;
using Kernel.CQRS.Transport;
using Kernel.Serialisation;

namespace CQRS.InMemoryTransport
{
    internal class TransportHandler : ITranspontHandler
    {
        private readonly ISerializer _serialiser;
        private readonly InMemoryTransport _transport;
        public TransportHandler(InMemoryTransport transport, ISerializer serialiser)
        {
            this._serialiser = serialiser;
            this._transport = transport;
        }
        public Task SentMessage<TMessage>(TMessage message) where TMessage : Message
        {
            using (var ms = new MemoryStream())
            {
                this._serialiser.Serialize(ms, new object[] { message });
                var serialsed = ms.ToArray();
                this._transport.Enque(serialsed);

            }
            return Task.CompletedTask;
        }
    }
}
