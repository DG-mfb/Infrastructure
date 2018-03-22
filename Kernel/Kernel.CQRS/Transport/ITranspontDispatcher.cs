using System.Threading.Tasks;
using Kernel.CQRS.Messaging;

namespace Kernel.CQRS.Transport
{
    public interface ITranspontDispatcher
    {
        ITransportManager TransportManager { get; }
        Task SendMessage<TMessage>(TMessage message) where TMessage : Message;
    }
}
