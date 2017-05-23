using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Importing.Infrastructure;
using Data.Importing.Infrastructure.Contexts;
using Data.Importing.Tests.MockData.Contexts;
using Data.Importing.Tests.MockData.DependencyResolvers;

namespace Data.Importing.Tests.MockData.StageProcessors
{
    internal class DownloadStageProcessor : MockStageProcessor
    {
        public override ImportStage<ImportStages> Stage
        {
            get
            {
                return new ImportStage<ImportStages>(ImportStages.Download);
            }
        }

        public DownloadStageProcessor(Action action) : base(new MockDependencyResolver(), action)
        {
            
        }
        public override StageResultContext Process(ImportContext context)
        {
            base.Action();
            return new MockStageResultContext();
        }
    }
}