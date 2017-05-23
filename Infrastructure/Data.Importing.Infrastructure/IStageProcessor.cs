using System;
using Data.Importing.Infrastructure.Contexts;

namespace Data.Importing.Infrastructure
{
    public interface IStageProcessor
    {
        StageResultContext Process(ImportContext context);
    }
}