using System.Threading.Tasks;
using Kernel.CQRS.Messaging;

namespace Kernel.CQRS.Transport
{
    public interface ITranspontHandler
    {
        Task SentMessage<TMassage>(TMassage message) where TMassage : Message;
    }
}
