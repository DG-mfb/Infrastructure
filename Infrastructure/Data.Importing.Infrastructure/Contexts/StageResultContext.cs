using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kernel.Messaging.Response;

namespace Data.Importing.Infrastructure.Contexts
{
    public abstract class StageResultContext : AbstractResponse
    {
        public ImportContext ImportContext { get; private set; }
        public IStageProcessor StageProcessor { get; private set; }

        public StageResultContext(ImportContext importContext, IStageProcessor stageProcessor)
        {
            this.ImportContext = importContext;
            this.StageProcessor = stageProcessor;
        }
    }
}