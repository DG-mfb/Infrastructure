using System;
using System.Threading;
using System.Threading.Tasks;
using CircuitBreakerInfrastructure;

namespace CircuitBreaker.StateManagers
{
    internal class StateManager : IStateManager
    {
        private BreakerState _state;
        private ITimeManager _timeManager;
        private IStateProvider _stateProvider;
        public StateManager(ITimeManager timeManager, IStateProvider stateProvider)
        {
            this._timeManager = timeManager;
            this._stateProvider = stateProvider;
        }
        public BreakerState CurrentState
        {
            get
            {
                return this._state;
            }

            set
            {
                this._state = value;
            }
        }
        
        public Task<IExecutionResult> Execute(BreakerExecutionContext executionContext)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            var closedState = this._stateProvider.GetState(State.Close, this);
            var haldOpenState = this._stateProvider.GetState(State.HalfOpen, this);
            Interlocked.CompareExchange(ref this._state, closedState, haldOpenState);
        }

        public void HalfOpen()
        {
            var openState = this._stateProvider.GetState(State.Open, this);
            var halfOpenState = this._stateProvider.GetState(State.HalfOpen, this);
            Interlocked.CompareExchange(ref this._state, halfOpenState, openState);
        }

        public void Open()
        {
            var openState = this._stateProvider.GetState(State.Open, this);
            Interlocked.Exchange(ref this._state, openState);
        }
    }
}