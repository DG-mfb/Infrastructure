using System.Threading.Tasks;
using Kernel.Federation.MetaData.Configuration.EntityDescriptors;

namespace Kernel.Federation.MetaData
{
    public interface IMetadataGenerator
    {
        Task CreateMetadata(EntityDesriptorConfiguration configuration);
        Task CreateMetadata(MetadataType metadataType);
    }
}