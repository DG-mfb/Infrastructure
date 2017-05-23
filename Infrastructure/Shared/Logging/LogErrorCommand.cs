namespace Shared.Logging
{
	using System;
	using Kernel;

    [Serializable]
    public class LogErrorCommand : ICommand
    {
		public Guid Id { get; set; }
        public string Source { get; set; }
        public string MachineName { get; set; }
        public string[] ErrorDetails { get; set; }
	}
}
