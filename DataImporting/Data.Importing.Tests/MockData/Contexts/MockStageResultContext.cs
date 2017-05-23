using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Importing.Infrastructure;
using Data.Importing.Infrastructure.Contexts;

namespace Data.Importing.Tests.MockData.Contexts
{
    public class MockStageResultContext : StageResultContext
    {
        public MockStageResultContext(ImportContext importContext, IStageProcessor stageProcessor) : base(importContext, stageProcessor)
        {
        }
    }
}
