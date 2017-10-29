using System;
using System.Threading.Tasks;
using CircuitBreakerInfrastructure;

namespace CircuitBreaker.States
{
    internal class HalfOpenState : BreakerState
    {
        public HalfOpenState(IStateManager stateManager) : base(stateManager)
        {
        }

        public override State State
        {
            get
            {
                return State.HalfOpen;
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