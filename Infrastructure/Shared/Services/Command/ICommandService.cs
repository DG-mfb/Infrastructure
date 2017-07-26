using System.ServiceModel;
using System.Threading.Tasks;
using Kernel.CQRS;

namespace Shared.Services.Command
{
    [ServiceContract]
	public interface ICommandService
	{
		/// <summary>
		/// Processes the command.
		/// </summary>
		/// <typeparam name="TCommand">The type of the command.</typeparam>
		/// <param name="command">The command.</param>
		/// <returns></returns>
		Task<object> ProcessCommand<TCommand>(TCommand command) where TCommand : ICommand;
	}
}
