using System.Threading.Tasks;

namespace CircuitBreakerInfrastructure
{
    public interface IBreakerProxy
    {
        BreakerState CurrentState { get; }
        void Open();
        void HalfOpen();
        void Close();
        Task<IBrakerResponse> Execute(BreakerExecutionContext executionContext);
    }
}