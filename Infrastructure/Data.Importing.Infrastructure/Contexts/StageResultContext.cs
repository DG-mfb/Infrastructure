using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kernel.Messaging.Response;

namespace Data.Importing.Infrastructure.Contexts
{
    public class StageResultContext
    {
        private Lazy<Task<StageResult>> _lazyResult;
        private StageImportContext ImportContext;
        private IStageProcessor StageProcessor;
        
        public Task<StageResult> Result { get { return this._lazyResult.Value; } }
        public StageResultContext(StageImportContext importContext, IStageProcessor stageProcessor)
        {
            this.ImportContext = importContext;
            this.StageProcessor = stageProcessor;
            this._lazyResult = new Lazy<Task<StageResult>>(new Func<Task<StageResult>>(() => stageProcessor.GetResultAsync(importContext)));
        }
    }
}