using System;
using System.Threading.Tasks;
using Kernel.CQRS.Dispatching;
using Kernel.CQRS.Messaging;
using Kernel.CQRS.Transport;

namespace CQRS.MessageDistpacher
{
    public class CommandDistpacher : IMessageDispatcher
    {
        private readonly ITranspontHandler _transpontHandler;

        public CommandDistpacher(ITranspontHandler transpontHandler)
        {
            this._transpontHandler = transpontHandler;
        }
        public Task SendMessage<TMessage>(TMessage message) where TMessage : Message
        {
            return Task.Factory.StartNew(async () =>
            {
                try
                {
                    await _transpontHandler.SentMessage(message);
                }
                catch (Exception e)
                {
                    HandleError(message, e);
                }
            });
        }

        private void HandleError(Message message, Exception e)
        {
            throw new NotImplementedException();
        }
    }
}