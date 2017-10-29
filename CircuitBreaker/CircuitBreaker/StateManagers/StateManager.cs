using System;
using System.Threading;
using System.Threading.Tasks;
using CircuitBreakerInfrastructure;

namespace CircuitBreaker.StateManagers
{
    internal class StateManager : IStateManager
    {
        private static bool _initialised;
        private BreakerState _state;
        private ITimeManager _timeManager;
        private IStateProvider _stateProvider;
        public StateManager(ITimeManager timeManager, IStateProvider stateProvider)
        {
            this._timeManager = timeManager;
            this._stateProvider = stateProvider;
            this.Initialise();
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
            return this.CurrentState.Execute(executionContext);
        }

        public void Close()
        {
            var closedState = this._stateProvider.GetState(State.Close, this);
            closedState.Enter();
            Interlocked.Exchange(ref this._state, closedState);
        }

        public void HalfOpen()
        {
            //var openState = this._stateProvider.GetState(State.Open, this);
            var halfOpenState = this._stateProvider.GetState(State.HalfOpen, this);
            halfOpenState.Enter();
            Interlocked.Exchange(ref this._state, halfOpenState);
        }

        public void Open()
        {
            var openState = this._stateProvider.GetState(State.Open, this);
            openState.Enter();
            Interlocked.Exchange(ref this._state, openState);
        }

        private void Initialise()
        {
            if (StateManager._initialised)
                throw new InvalidOperationException("State manager must be initialised once only. If resoved from DI container register it as a singleton");

            var closedState = this._stateProvider.GetState(State.Close, this);

            var previous = Interlocked.CompareExchange(ref this._state, closedState, null);
            if(previous != null)
                throw new InvalidOperationException("State manager must be initialised once only. If resoved from DI container register it as a singleton");

            StateManager._initialised = true;
        }
    }
}