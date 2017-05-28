using System;
using System.Threading.Tasks;
using Data.Importing.Infrastructure.Contexts;

namespace Data.Importing.Infrastructure
{
    public interface IStageProcessor
    {
        StageResultContext GetResult(StageImportContext context);
        Task<StageResultContext> GetResultAsync(StageImportContext context);
    }
}