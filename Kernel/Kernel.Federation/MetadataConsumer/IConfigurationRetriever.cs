using System.Threading;
using System.Threading.Tasks;

namespace Kernel.Federation.MetadataConsumer
{
    public interface IConfigurationRetriever<T>
    {
        Task<T> GetAsync(string address, CancellationToken cancel);
    }
}