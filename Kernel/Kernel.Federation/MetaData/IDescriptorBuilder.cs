namespace Kernel.Federation.MetaData
{
    public interface IDescriptorBuilder<T> where T : class
    {
        T BuildDescriptor(IMetadataConfiguration configuration);
    }
}