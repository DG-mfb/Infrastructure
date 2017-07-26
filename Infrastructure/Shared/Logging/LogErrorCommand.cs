namespace Shared.Logging
{
	using System;
	using Kernel;
    using Kernel.CQRS.Command;

    [Serializable]
    public class LogErrorCommand : Command
    {
        public LogErrorCommand(Guid id, Guid correlationId) : base(id, correlationId)
        {
        }

        public string Source { get; set; }
        public string MachineName { get; set; }
        public string[] ErrorDetails { get; set; }
	}
}
