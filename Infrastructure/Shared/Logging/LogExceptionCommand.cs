namespace Shared.Logging
{
	using System;
	using System.Collections.Generic;
	using Kernel.Command;

    [Serializable]
	public class LogExceptionCommand : CommandBase
    {
        public Dictionary<string, string> Parameters { get; set; }
        public ExceptionIdentityContext UserDetails { get; set; }
        public string MachineName { get; set; }
        public Exception Exception { get; set; }
        public string TargetMethodName { get; set; }
        public string TargetType { get; set; }
		public Guid Id { get; set; }
	}
}
