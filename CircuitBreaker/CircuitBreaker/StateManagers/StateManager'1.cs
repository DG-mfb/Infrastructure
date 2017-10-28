using System;
using System.Threading.Tasks;
using CircuitBreakerInfrastructure;

namespace CircuitBreaker.StateManagers
{
    internal abstract class StateManager<TState> : IStateManager where TState : BreakerState
    {
        private TState _state;
        private IBreakerProxy _breaker;
        private ITimeManager _timeManager;
        protected StateManager(TState state, ITimeManager timeManager, IBreakerProxy breaker)
        {
            this._state = state;
            this._timeManager = timeManager;
            this._breaker = breaker;
        }
        public BreakerState State
        {
            get
            {
                return this._state;
            }
        }
        

        public Task<IExecutionResult> Execute(BreakerExecutionContext executionContext)
        {
            return this.ExecuteInternal(executionContext);
        }

        protected abstract Task<IExecutionResult> ExecuteInternal(BreakerExecutionContext executionContext);
    }
}