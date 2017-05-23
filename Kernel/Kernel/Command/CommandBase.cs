namespace Kernel.Command
{
	using System;

	public class CommandBase : ICommand
	{
		public Guid Id { get; set; }
	}
}