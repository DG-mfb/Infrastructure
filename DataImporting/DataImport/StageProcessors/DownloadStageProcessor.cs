using System;
using Data.Importing.Infrastructure;
using Data.Importing.Infrastructure.Contexts;
using Kernel.DependancyResolver;

namespace Data.Importing.StageProcessors
{
    internal class DownloadStageProcessor : StageProcessor
    {
        public override ImportStage<ImportStages> Stage
        {
            get
            {
                return new ImportStage<ImportStages>(ImportStages.Download);
            }
        }

        public DownloadStageProcessor(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
        }
        public override StageResultContext Process(ImportContext context)
        {
            throw new NotImplementedException();
        }
    }
}