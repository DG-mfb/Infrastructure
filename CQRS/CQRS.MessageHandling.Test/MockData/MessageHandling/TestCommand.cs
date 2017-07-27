using System;
using Kernel.CQRS.Command;

namespace CQRS.MessageHandling.Test.MockData.MessageHandling
{
    internal class TestCommand : Command
    {
        public TestCommand(Guid id, Guid correlationId) : base(id, correlationId)
        {
        }
    }
}
