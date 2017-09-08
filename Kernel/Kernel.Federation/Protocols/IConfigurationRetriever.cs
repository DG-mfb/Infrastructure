using System.Threading;
using System.Threading.Tasks;

namespace Kernel.Federation.Protocols
{
    public interface IConfigurationRetriever<T>
    {
        Task<T> GetConfigurationAsync(string address, IDocumentRetriever retriever, CancellationToken cancel);
    }
}