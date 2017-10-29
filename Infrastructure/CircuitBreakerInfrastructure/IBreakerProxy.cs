using System.Threading.Tasks;

namespace CircuitBreakerInfrastructure
{
    public interface IBreakerProxy
    {
        IStateManager StateManager { get; }
        
        Task<IBrakerResponse> Execute(BreakerExecutionContext executionContext);
    }
}