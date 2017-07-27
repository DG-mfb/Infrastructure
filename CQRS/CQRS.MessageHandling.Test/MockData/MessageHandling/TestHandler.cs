using System;
using System.Threading.Tasks;
using Kernel.CQRS.MessageHandling.CommandHandling;

namespace CQRS.MessageHandling.Test.MockData.MessageHandling
{
    internal class TestHandler : ICommandHandler<TestCommand>
    {
        private readonly Action _action;
        public  TestHandler(Action action)
        {
            this._action = action;
        }

        public Task Handle(TestCommand command)
        {
            this._action();
            return Task.CompletedTask;
        }
    }
}