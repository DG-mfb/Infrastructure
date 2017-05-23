using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Data.Importing.Infrastructure.Contexts
{
    public class ImportContext
    {
        public IDictionary<IStageProcessor, StageResultContext> Results { get; private set; }
        public ResourceContext SourceContext { get; private set; }
        public TargetContext TargetContext { get; private set; }

        public ImportContext(ResourceContext resource, TargetContext target)
        {
            this.SourceContext = resource;
            this.TargetContext = target;
            this.Results = new Dictionary<IStageProcessor, StageResultContext>();
        }
    }
}