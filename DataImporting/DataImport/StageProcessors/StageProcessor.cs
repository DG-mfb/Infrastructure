using System;
using System.Threading.Tasks;
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

        public StageResult GetResult(StageImportContext context)
        {
            var result = Task.Factory.StartNew<Task<StageResult>>(async () => await this.GetResultAsync(context));
            result.Wait();
            return result.Result.Result;
        }

        public async Task<StageResult> GetResultAsync(StageImportContext context, Func<StageImportContext, Task<StageResult>> next)
        {
            var result = await this.GetResultAsync(context);
            if (result.IsCompleted)
                return result;
            return await next(context);
        }

        public abstract Task<StageResult> GetResultAsync(StageImportContext context);
    }
}