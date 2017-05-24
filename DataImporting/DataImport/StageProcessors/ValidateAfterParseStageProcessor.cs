﻿using System;
using System.Threading.Tasks;
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
        public override Task<StageResult> GetResultAsync(StageImportContext context)
        {
            throw new NotImplementedException();
        }
    }
}