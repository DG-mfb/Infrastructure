using System;
using System.Threading.Tasks;
using CQRS.Infrastructure.Messaging;
using Kernel.CQRS.Dispatching;
using Kernel.CQRS.Messaging;
using Kernel.CQRS.Transport;
using Kernel.Logging;

namespace CQRS.MessageDistpacher
{
    public class CommandDispatcher : IMessageDispatcher<Command>
    {
        private readonly ITranspontDispatcher _transpontHandler;
        private readonly ILogProvider _logProvider;

        public CommandDispatcher(ITranspontDispatcher transpontHandler, ILogProvider logProvider)
        {
            this._transpontHandler = transpontHandler;
            this._logProvider = logProvider;
        }
        Task IMessageDispatcher.SendMessage(Message message)
        {
            return Task.Factory.StartNew(async () =>
            {
                try
                {
                    await _transpontHandler.SendMessage(message);
                }
                catch (Exception e)
                {
                    HandleError(message, e);
                }
            });
        }

        public Task SendMessage(Command message)
        {
            return ((IMessageDispatcher)this).SendMessage(message);
        }

        private void HandleError(Message message, Exception e)
        {
            Exception inner;
            this._logProvider.TryLogException(e, out inner);
        }
    }
}