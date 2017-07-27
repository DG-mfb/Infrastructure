using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS.MessageHandling.Invocation;
using Kernel.CQRS.Command;
using Kernel.CQRS.MessageHandling.CommandHandling;
using NUnit.Framework;

namespace CQRS.MessageHandling.Test
{
    internal class TestCommand : Command
    {
        public TestCommand(Guid id, Guid correlationId) : base(id, correlationId)
        {
        }
    }
    internal class Handler : ICommandHandler<TestCommand>
    {
        public Task Handle(TestCommand command)
        {
            throw new NotImplementedException();
        }
    }

    [TestFixture]
    public class HandlerFactoryTests
    {
        [Test]
        public void Test11()
        {
            //ARRANGE
            var command = new TestCommand(Guid.NewGuid(), Guid.NewGuid());

            var handler = new Handler();
            //ACT
            var del = HandlerFactory.BuildMessageHandlerDelegate(typeof(Handler), typeof(TestCommand));
            del(handler, new[] { command });
            //ASSERT
        }
    }
}
