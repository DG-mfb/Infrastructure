using System.Threading.Tasks;

namespace Kernel.CQRS.Transport
{
    public interface ITransportManager
    {
        Task Initialise();
        Task Start();
        Task Stop();
    }
}