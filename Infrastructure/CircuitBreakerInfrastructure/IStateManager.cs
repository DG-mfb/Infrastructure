using System.Threading.Tasks;

namespace CircuitBreakerInfrastructure
{
    public interface IStateManager
    {
        BreakerState CurrentState { get; }

        void Open();
        void HalfOpen();
        void Close();

        Task<IExecutionResult> Execute(BreakerExecutionContext executionContext);
    }
}