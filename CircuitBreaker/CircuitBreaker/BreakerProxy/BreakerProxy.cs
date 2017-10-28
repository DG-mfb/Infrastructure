using System.Threading;
using System.Threading.Tasks;
using CircuitBreakerInfrastructure;

namespace CircuitBreaker.BreakerProxy
{
    internal abstract class BreakerProxy : IBreakerProxy
    {
        private IStateProvider _stateProvider;
        private IStateManager _stateManager;
        protected BreakerProxy(IStateProvider stateProvider)
        {
            this._stateProvider = stateProvider;
        }

        public BreakerState CurrentState
        {
            get
            {
                return this._stateManager.State;
            }
        }

        public async Task<IBrakerResponse> Execute(BreakerExecutionContext executionContext)
        {
            var result = await this._stateManager.Execute(executionContext);
            return result.Execute(this);
        }

        public void Close()
        {
            var closedState = this._stateProvider.GetState(State.Close);
            var haldOpenState = this._stateProvider.GetState(State.HalfOpen);
            Interlocked.CompareExchange(ref this._stateManager, closedState, haldOpenState);
        }

        public void HalfOpen()
        {
            var openState = this._stateProvider.GetState(State.Open);
            var halfOpenState = this._stateProvider.GetState(State.HalfOpen);
            Interlocked.CompareExchange(ref this._stateManager, halfOpenState, openState);
        }

        public void Open()
        {
            var openState = this._stateProvider.GetState(State.Open);
            Interlocked.Exchange(ref this._stateManager, openState);
        }
    }
}