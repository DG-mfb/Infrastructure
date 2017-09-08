using System.Threading;
using System.Threading.Tasks;

namespace Kernel.Federation.Protocols
{
    public interface IConfigurationManager<T> where T : class
    {
        Task<T> GetConfigurationAsync(CancellationToken cancel);

        void RequestRefresh();
    }
}