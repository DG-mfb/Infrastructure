using System;
using System.Linq;
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

            var seed = new Func<ImportContext, StageResultContext>(c =>
            {
                return new MockStageResultContext(c, null);
            });
            var context = new ImportContext(null, null);
            var del = instances.Aggregate(seed, (f, next) => new Func<ImportContext, StageResultContext>(c => next.Process(c, f)));
            var res = del(context);
        }
    }
}
