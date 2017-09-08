using System.Threading;
using System.Threading.Tasks;

namespace Kernel.Federation.Protocols
{
    public interface IDocumentRetriever
    {
        Task<string> GetDocumentAsync(string address, CancellationToken cancel);
    }
}