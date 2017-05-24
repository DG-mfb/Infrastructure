using System;
using System.Threading.Tasks;
using Data.Importing.Infrastructure.Contexts;

namespace Data.Importing.Infrastructure
{
    public interface IStageProcessor
    {
        StageResult GetResult(StageImportContext context);
        Task<StageResult> GetResultAsync(StageImportContext context);
    }
}