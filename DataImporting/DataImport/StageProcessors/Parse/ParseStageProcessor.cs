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
        public override Task<StageResultContext> GetResultAsync(StageImportContext context)
        {
            throw new NotImplementedException();
        }
    }
}