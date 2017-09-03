using Kernel.Federation.MetaData;

namespace SPMetadataProvider.Metadata
{
    public class SingleSignOnServiceContext : ISingleSignOnServiceContext
    {
        public string Location { get; set; }

        public string Binding { get; set; }
    }
}