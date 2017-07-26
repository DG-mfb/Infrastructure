namespace Shared.Logging
{
	using System;
	using System.Collections.Generic;
	using Kernel.CQRS.Command;

    [Serializable]
	public class LogExceptionCommand : Command
    {
        public LogExceptionCommand(Guid id, Guid correlationId) : base(id, correlationId)
        {
        }

        public Dictionary<string, string> Parameters { get; set; }
        public ExceptionIdentityContext UserDetails { get; set; }
        public string MachineName { get; set; }
        public Exception Exception { get; set; }
        public string TargetMethodName { get; set; }
        public string TargetType { get; set; }
	}
}
