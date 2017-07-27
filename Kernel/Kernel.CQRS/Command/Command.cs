namespace Kernel.CQRS.Command
{
	using System;
    using Kernel.CQRS.Messaging;

    public class Command : Message
	{
        public Command(Guid id, Guid correlationId) : base(id, correlationId)
        {
        }
	}
}