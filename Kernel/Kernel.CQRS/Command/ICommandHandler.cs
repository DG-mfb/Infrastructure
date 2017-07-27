namespace Kernel.CQRS
{
	using Kernel.Initialisation;

	/// <summary>
	/// Provides methods to handle commands
	/// </summary>
	/// <typeparam name="TCommand">The type of the command.</typeparam>
	public interface ICommandHandler<TCommand> : IAutoRegisterAsTransient where TCommand : Command.Command
	{
		/// <summary>
		/// Handles the specified command.
		/// </summary>
		/// <param name="command">The command.</param>
		void Handle(TCommand command);
	}
}