﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kernel.CQRS.Transport;

namespace CQRS.InMemoryTransport
{
    internal class TransportManager : ITransportManager
    {
        private readonly InMemoryQueueTransport _transport;
        internal ICollection<Func<byte[], Task>> MessageListeners;
        public TransportManager(InMemoryQueueTransport transport)
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

        public Task RegisterListener(IMessageListener listener)
        {
            this.MessageListeners.Add(listener.RecieveMessage);
            return Task.CompletedTask;
        }

        public Task Start()
        {
            return this._transport.Start();
        }

        public Task Stop()
        {
            return this._transport.Stop();
        }

        public Task MessageReceived()
        {
            byte[] message;
            this._transport.TryDequeue(out message);
            Parallel.ForEach(this.MessageListeners, s => s(message));
            return Task.CompletedTask;
        }
    }
}