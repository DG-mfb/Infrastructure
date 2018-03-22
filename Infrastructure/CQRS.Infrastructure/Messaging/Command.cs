﻿namespace CQRS.Infrastructure.Messaging
{
	using System;
    using Kernel.CQRS.Messaging;

    [Serializable]
    public class Command : Message
	{
        public Command(Guid id, Guid correlationId) : base(id, correlationId)
        {
        }
	}
}