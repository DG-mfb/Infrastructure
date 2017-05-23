using System;
using Data.Importing.Infrastructure;
using Data.Importing.Infrastructure.Contexts;
using Data.Importing.StageProcessors;
using Data.Importing.Tests.MockData.Contexts;
using Data.Importing.Tests.MockData.DependencyResolvers;

namespace Data.Importing.Tests.MockData.StageProcessors
{
    internal class ValidateAfterParseStageProcessor : MockStageProcessor
    {
        public override ImportStage<ImportStages> Stage
        {
            get
            {
                return new ImportStage<ImportStages>(ImportStages.ValidateLevel1);
            }
        }

        public ValidateAfterParseStageProcessor(Action action) : base(new MockDependencyResolver(), action)
        {
            
        }
        public override StageResultContext Process(ImportContext context)
        {
            this.Action();
            return new MockStageResultContext();
        }
    }
}