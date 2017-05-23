using System.Collections.Generic;

namespace Data.Importing.Infrastructure.Contexts
{
    public class ImportContext
    {
        public ICollection<StageResultContext> Results { get; private set; }
        public ResourceContext SourceContext { get; private set; }
        public TargetContext TargetContext { get; private set; }

        public ImportContext(ResourceContext resource, TargetContext target)
        {
            this.SourceContext = resource;
            this.TargetContext = target;
            this.Results = new List<StageResultContext>();
        }
    }
}