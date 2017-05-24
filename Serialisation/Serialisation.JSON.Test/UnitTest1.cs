using System;
using System.IO;
using Kernel.Serialisation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serialisation.JSON.SettingsProviders;

namespace Serialisation.JSON.Test
{
    [TestClass]
    public class UnitTest1
    {
        internal class Test
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        [TestMethod]
        public void TestMethod1()
        {
            var setting = new DefaultSettingsProvider();
            var ser = new NSJsonSerializer(setting);
            var o = ser.Serialize(new Test { Id = 1, Name = "Test" });
            //var o = ser.Serialize(new { Id = 1, Name = "Test" });
            var sererialised = ser.Deserialize<object>(o);
            var t = sererialised.GetType();
        }
    }
}