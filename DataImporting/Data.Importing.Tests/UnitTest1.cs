using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Importing.Infrastructure;
using Data.Importing.Infrastructure.Contexts;
using Data.Importing.StageProcessors;
using Data.Importing.Tests.MockData.Contexts;
using Kernel.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Data.Importing.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var processorTypes = ReflectionHelper.GetAllTypes(new[] { this.GetType().Assembly }, 
                t => !t.IsAbstract && !t.IsInterface && typeof(IStageProcessor<ImportStages>).IsAssignableFrom(t))
                .ToList();
            var instances = processorTypes.Select(x => Activator.CreateInstance(x, new Action(() => { })) as StageProcessor)
                .OrderByDescending(o => o.Stage)
                .ToList();

            var seed = new Func<StageImportContext, Task<StageResult>>(c =>
            {
                return Task.FromResult(new StageResult(null));
            });
            var context = new ImportContext(null, null);
            var stageContext = new StageImportContext(null, context);
            var del = instances.Aggregate(seed, (f, next) => new Func<StageImportContext, Task<StageResult>>(c => next.GetResultAsync(c, f)));
            var res = del(stageContext);
        }
    }
}
