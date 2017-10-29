using System;
using System.Threading.Tasks;
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

        public override Task Enter()
        {
            throw new NotImplementedException();
        }

        public override Task<IExecutionResult> Execute(BreakerExecutionContext executionContext)
        {
            throw new NotImplementedException();
        }

        public override Task Exit()
        {
            throw new NotImplementedException();
        }
    }
}