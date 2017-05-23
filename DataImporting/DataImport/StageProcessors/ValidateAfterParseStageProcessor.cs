using System;
using Data.Importing.Infrastructure;
using Data.Importing.Infrastructure.Contexts;
using Kernel.DependancyResolver;

namespace Data.Importing.StageProcessors
{
    internal class ValidateAfterParseStageProcessor : StageProcessor
    {
        public override ImportStage<ImportStages> Stage
        {
            get
            {
                return new ImportStage<ImportStages>(ImportStages.ValidateLevel1);
            }
        }

        public ValidateAfterParseStageProcessor(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
        }
        public override StageResultContext Process(ImportContext context)
        {
            throw new NotImplementedException();
        }
    }
}