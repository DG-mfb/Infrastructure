using System;
using System.Threading;
using System.Threading.Tasks;
using CircuitBreaker.ExecutionResults;
using CircuitBreakerInfrastructure;
using Kernel.Extensions;
using Kernel.Reflection;
//using Kernel.Reflection.Extensions;

namespace CircuitBreaker.States
{
    internal class CloseState : BreakerState
    {
        private int _failCount;
        private readonly Timer _timer;
        public CloseState(IStateManager stateManager) : base(stateManager)
        {
            this._timer = new Timer(new TimerCallback( o => { Interlocked.Exchange(ref this._failCount, 0); }), this, TimeSpan.FromSeconds(20), TimeSpan.FromSeconds(20));
        }

        public override State State
        {
            get
            {
                return State.Close;
            }
        }

        protected override Task<IExecutionResult> ExecuteInternal(BreakerExecutionContext executionContext)
        {
            var t = executionContext.Action();
            
            t.Wait();

            return Task.FromResult<IExecutionResult>(new SuccessExecutionResult(base.GetResultFactory(t)));
        }

        protected override Task<IExecutionResult> Trip(Exception e, BreakerExecutionContext executionContext)
        {
            Interlocked.Increment(ref this._failCount);
            if (_failCount > 1)
                this.StateManager.Open();
            return Task.FromResult<IExecutionResult>( new FailedExecutionResult(() => null, this.StateManager.CurrentState.State, e));
        }
    }
}