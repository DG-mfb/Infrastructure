using System.Threading.Tasks;

namespace CircuitBreakerInfrastructure
{
    public interface IStateManager
    {
        BreakerState State { get; }
        
        Task<IExecutionResult> Execute(BreakerExecutionContext executionContext);
    }
}