using System;
using System.Linq;
using CQRS.MessageHandling.Factories;
using CQRS.MessageHandling.Invocation;
using CQRS.MessageHandling.Test.MockData;
using CQRS.MessageHandling.Test.MockData.MessageHandling;
using Kernel.DependancyResolver;
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

            var handler = new TestCommandHandler(() => result = 10);
            //ACT
            var del = HandlerDelegateFactory.BuildMessageHandlerDelegate(typeof(TestCommandHandler), typeof(TestCommand));
            del(handler, new[] { command });
            //ASSERT
            Assert.AreEqual(10, result);
        }

        [Test]
        public void HandlerFactoryTest()
        {
            //ARRANGE
            var result = 0;
            var command = new TestEvent(Guid.NewGuid(), Guid.NewGuid());
            var dependencyResolver = new DependencyResolverMock();
            var handlerFactorySettings = new HandlerFactorySettingsMock();
            var handlerFactory = new HandlerFactory(dependencyResolver, handlerFactorySettings);
            //var handler = new TestCommandHandler(() => result = 10);
            dependencyResolver.RegisterFactory<Action>(t => () => result = 10, Lifetime.Singleton);
            //ACT
            var handler = handlerFactory.GetAllHandlersFor(typeof(TestCommand));
            
            //ASSERT
            Assert.IsInstanceOf<TestCommandHandler>(handler.Single());
        }
    }
}
