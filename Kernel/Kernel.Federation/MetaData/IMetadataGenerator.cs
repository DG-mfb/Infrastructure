using System.Threading.Tasks;

namespace Kernel.Federation.MetaData
{
    public interface IMetadataGenerator
    {
        Task CreateMetadata(IMetadataConfiguration configuration);
        Task CreateMetadata();
    }
}