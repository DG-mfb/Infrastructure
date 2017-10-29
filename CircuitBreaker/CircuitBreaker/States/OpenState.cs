using System;
using System.Threading;
using System.Threading.Tasks;
using CircuitBreaker.ExecutionResults;
using CircuitBreakerInfrastructure;

namespace CircuitBreaker.States
{
    internal class OpenState : BreakerState
    {
        private readonly Timer _timer;
        public OpenState(IStateManager stateManager) : base(stateManager)
        {
            this._timer = new Timer(new TimerCallback(o => 
            {
                this.Exit();
                this.StateManager.HalfOpen();
            }), this, TimeSpan.FromMilliseconds(-1), TimeSpan.FromMilliseconds(-1));
        }

        public override State State
        {
            get
            {
                return State.Open;
            }
        }
        public override Task Enter()
        {
            this._timer.Change(TimeSpan.FromSeconds(5), TimeSpan.FromMilliseconds(-1));
            return base.Enter();
        }

        public override Task Exit()
        {
            this._timer.Change(TimeSpan.FromMilliseconds(-1), TimeSpan.FromMilliseconds(-1));
            return base.Exit();
        }
        protected override Task<IExecutionResult> ExecuteInternal(BreakerExecutionContext executionContext)
        {
            return Task.FromResult<IExecutionResult>(new FailedExecutionResult(() => null, State.Open, null));
        }

        protected override Task<IExecutionResult> Trip(Exception e, BreakerExecutionContext executionContext)
        {
            throw new NotImplementedException();
        }
    }
}