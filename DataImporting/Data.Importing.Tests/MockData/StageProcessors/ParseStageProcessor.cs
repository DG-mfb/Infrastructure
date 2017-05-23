using System;
using Data.Importing.Infrastructure;
using Data.Importing.Infrastructure.Contexts;
using Data.Importing.Tests.MockData.Contexts;
using Data.Importing.Tests.MockData.DependencyResolvers;

namespace Data.Importing.Tests.MockData.StageProcessors
{
    internal class ParseStageProcessor : MockStageProcessor
    {
        

        public  override ImportStage<ImportStages> Stage
        {
            get
            {
                return new ImportStage<ImportStages>(ImportStages.Deserialise);
            }
        }

        public ParseStageProcessor(Action action) : base(new MockDependencyResolver(), action)
        {
            
        }
        public override StageResultContext Process(ImportContext context)
        {
            base.Action();
            return new MockStageResultContext(context, this);
        }
    }
}