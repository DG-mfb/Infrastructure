using System;
using CQRS.MessageHandling.Invocation;
using CQRS.MessageHandling.Test.MockData.MessageHandling;
using NUnit.Framework;

namespace CQRS.MessageHandling.Test.MessageHandling
{


    [TestFixture]
    public class HandlerFactoryTests
    {
        [Test]
        public void Test11()
        {
            //ARRANGE
            var result = 0;
            var command = new TestCommand(Guid.NewGuid(), Guid.NewGuid());

            var handler = new TestHandler(() => result = 10);
            //ACT
            var del = HandlerDelegateFactory.BuildMessageHandlerDelegate(typeof(TestHandler), typeof(TestCommand));
            del(handler, new[] { command });
            //ASSERT
            Assert.AreEqual(10, result);
        }
    }
}
