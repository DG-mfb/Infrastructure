using System.Threading;
using System.Threading.Tasks;
using CircuitBreakerInfrastructure;

namespace CircuitBreaker.BreakerProxy
{
    internal class BreakerProxy : IBreakerProxy
    {
        private IStateProvider _stateProvider;
        private BreakerState _state;
        protected BreakerProxy(IStateProvider stateProvider)
        {
            this._stateProvider = stateProvider;
            this._state = stateProvider.GetState(State.Close);
        }

        public BreakerState CurrentState
        {
            get
            {
                return this._state;
            }
        }

        public async Task<IBrakerResponse> Execute(BreakerExecutionContext executionContext)
        {
            var result = await this._state.Execute(executionContext);
            return result.Execute(this);
        }

        public void Close()
        {
            var closedState = this._stateProvider.GetState(State.Close);
            var haldOpenState = this._stateProvider.GetState(State.HalfOpen);
            Interlocked.CompareExchange(ref this._state, closedState, haldOpenState);
        }

        public void HalfOpen()
        {
            var openState = this._stateProvider.GetState(State.Open);
            var halfOpenState = this._stateProvider.GetState(State.HalfOpen);
            Interlocked.CompareExchange(ref this._state, halfOpenState, openState);
        }

        public void Open()
        {
            var openState = this._stateProvider.GetState(State.Open);
            Interlocked.Exchange(ref this._state, openState);
        }
    }
}