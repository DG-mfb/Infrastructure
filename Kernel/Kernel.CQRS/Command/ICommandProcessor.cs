﻿namespace Kernel.CQRS
{
	using System.Threading.Tasks;

	/// <summary>
	/// Exposes method to process serialized command
	/// </summary>
	public interface ICommandProcessor
	{
		Task ProcessCommand(string command);
		Task ProcessCommand(Command.Command command);
	}
}