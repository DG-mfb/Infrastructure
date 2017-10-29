using System;
using System.Threading.Tasks;
using CircuitBreaker.ExecutionResults;
using CircuitBreakerInfrastructure;

namespace CircuitBreaker.States
{
    internal class CloseState : BreakerState
    {
        public CloseState(IStateManager stateManager) : base(stateManager)
        {
        }

        public override State State
        {
            get
            {
                return State.Close;
            }
        }

        protected override async Task<IExecutionResult> ExecuteInternal(BreakerExecutionContext executionContext)
        {
            await executionContext.Action();
            return new SuccessExecutionResult(() => null);
        }

        protected override Task<IExecutionResult> Trip(Exception e, BreakerExecutionContext executionContext)
        {
            return Task.FromResult<IExecutionResult>( new FailedExecutionResult(null, e));
        }
    }
}