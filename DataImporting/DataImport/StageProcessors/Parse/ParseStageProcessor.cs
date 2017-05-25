using System;
using System.Threading.Tasks;
using Data.Importing.Helpers;
using Data.Importing.Infrastructure;
using Data.Importing.Infrastructure.Contexts;
using Kernel.DependancyResolver;
using Kernel.Serialisation;

namespace Data.Importing.StageProcessors
{
    internal class ParseStageProcessor : StageProcessor
    {
        private readonly ISerializer _serializer;

        public  override ImportStage<ImportStages> Stage
        {
            get
            {
                return new ImportStage<ImportStages>(ImportStages.Deserialise);
            }
        }

        public ParseStageProcessor(IDependencyResolver dependencyResolver, ISerializer serializer) : base(dependencyResolver)
        {
            this._serializer = serializer;
        }
        public override Task<StageResult> GetResultAsync(StageImportContext context)
        {
            StageResult result;
            var previousResult = context.Source;
            if (!previousResult.IsResultValid)
                result = previousResult;
            else
            {
                var del = StageResultHelper.GetDeserialiserDelegate(context.ImportContext.TargetContext.ParseTo);
                var deserialised = del(this._serializer, previousResult.Result);
                result = new StageResult(deserialised);
            }
            return Task.FromResult(result);
        }
    }
}