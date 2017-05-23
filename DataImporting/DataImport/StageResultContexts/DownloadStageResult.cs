using Data.Importing.Infrastructure;
using Data.Importing.Infrastructure.Contexts;

namespace Data.Importing.StageResultContexts
{
    public class DownloadStageResult : StageResultContext
    {
        private readonly string _content;

        public string Content
        {
            get
            {
                return this._content;
            }
        }
        public DownloadStageResult(ImportContext importContext, IStageProcessor stageProcessor, string content) 
            : base(importContext, stageProcessor)
        {
            this._content = content;
        }
    }
}