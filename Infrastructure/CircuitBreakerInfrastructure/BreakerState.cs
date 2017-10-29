using System.Threading.Tasks;

namespace CircuitBreakerInfrastructure
{
    public abstract class BreakerState
    {
        protected IStateManager StateManager;
        public abstract State State { get; }
        protected BreakerState()
        {
        }

        public abstract Task Enter();
        public abstract Task Exit();
        public abstract Task<IExecutionResult> Execute(BreakerExecutionContext executionContext);
    }
}