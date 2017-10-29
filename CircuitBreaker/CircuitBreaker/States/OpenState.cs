using System;
using System.Threading.Tasks;
using CircuitBreakerInfrastructure;

namespace CircuitBreaker.States
{
    internal class OpenState : BreakerState
    {
        public OpenState(IStateManager stateManager) : base(stateManager)
        {
        }

        public override State State
        {
            get
            {
                return State.Open;
            }
        }

        protected override Task<IExecutionResult> ExecuteInternal(BreakerExecutionContext executionContext)
        {
            throw new NotImplementedException();
        }

        protected override Task<IExecutionResult> Trip(Exception e, BreakerExecutionContext executionContext)
        {
            throw new NotImplementedException();
        }
    }
}