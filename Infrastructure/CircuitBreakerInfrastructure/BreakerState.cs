using System.Threading.Tasks;

namespace CircuitBreakerInfrastructure
{
    public abstract class BreakerState
    {
        protected IStateManager StateManager;
        protected BreakerState(IStateManager stateManager)
        {
            this.StateManager = stateManager;
        }

        public abstract State State { get; }
        
        public abstract Task Enter();
        public abstract Task Exit();
        public abstract Task<IExecutionResult> Execute(BreakerExecutionContext executionContext);
    }
}