namespace Kernel
{
	using System.Threading.Tasks;

	/// <summary>
	/// Exposes method to process serialized command
	/// </summary>
	public interface ICommandProcessor
	{
		Task ProcessCommand(string command);
		Task ProcessCommand(ICommand command);
	}
}