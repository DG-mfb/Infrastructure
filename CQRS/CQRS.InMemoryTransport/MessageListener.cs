using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kernel.CQRS.Transport;

namespace CQRS.InMemoryTransport
{
    internal class MessageListener : IMessageListener
    {
        public Task<bool> AttachTo(ITransportManager transportManager)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Start()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Stop()
        {
            throw new NotImplementedException();
        }
    }
}