using System;
using Data.Importing.Infrastructure;
using Data.Importing.Infrastructure.Contexts;
using Data.Importing.StageProcessors;
using Data.Importing.Tests.MockData.DependencyResolvers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serialisation.JSON;
using Serialisation.JSON.SettingsProviders;

namespace Data.Importing.Tests.StageProcessors
{
    internal class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [TestClass]
    public class ParseStageProcessorTests
    {

        [TestMethod]
        public void TestMethod1()
        {
            //ARRANGE
            var resolver = new MockDependencyResolver();
            var setting = new DefaultSettingsProvider();
            var ser = new NSJsonSerializer(setting);
            var processor = new ParseStageProcessor(resolver, ser);
            var targetContext = new TargetContext { ParseTo = typeof(Test) };
            var importContext = new ImportContext(null, targetContext);
            var o = ser.Serialize(new Test { Id = 1, Name = "Test" });
            var result = new StageResult(o);
            result.Validated();
            var stageContext = new StageImportContext(result, importContext);
            //ACT
            var r = processor.GetResultAsync(stageContext).Result;
            //ASSERT
        }
    }
}
