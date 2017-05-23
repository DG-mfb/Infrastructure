using System;
using Data.Importing.Infrastructure;
using Data.Importing.Infrastructure.Contexts;
using Kernel.DependancyResolver;

namespace Data.Importing.StageProcessors
{
    internal abstract class StageProcessor : IStageProcessor<ImportStages>
    {
        protected readonly IDependencyResolver DependencyResolver;

        public abstract ImportStage<ImportStages> Stage { get; }

        public StageProcessor(IDependencyResolver dependencyResolver)
        {
            this.DependencyResolver = dependencyResolver;
        }
        public StageResultContext Process(ImportContext context, Func<ImportContext, StageResultContext> next)
        {
            var result = this.Process(context);
            context.Results.Add(this, result);
            return next(context);
        }

        public abstract StageResultContext Process(ImportContext context);
    }
}