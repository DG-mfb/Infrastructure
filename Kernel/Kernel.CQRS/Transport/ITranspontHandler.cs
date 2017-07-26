using System.Threading.Tasks;
using Kernel.CQRS.Messaging;

namespace Kernel.CQRS.Transport
{
    public interface ITranspontHandler
    {
        Task SentMessage<TMessage>(TMessage message) where TMessage : Message;
    }
}
