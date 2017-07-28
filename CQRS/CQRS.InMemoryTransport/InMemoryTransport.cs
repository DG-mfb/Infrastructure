﻿using System;
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
        private ConcurrentQueue<byte[]> _queue;
        private bool _isStarted;

        public bool IsEmpty
        {
            get
            {
                return this._queue.IsEmpty;
            }
        }

        public InMemoryTransport()
        {
            this._queue = new ConcurrentQueue<byte[]>();
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