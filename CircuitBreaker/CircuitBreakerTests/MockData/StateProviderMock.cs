using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CircuitBreaker.BreakerProxy;
using CircuitBreaker.StateManagers;
using CircuitBreaker.States;
using CircuitBreakerInfrastructure;

namespace CircuitBreakerTests.MockData
{
    class StateProviderMock : IStateProvider
    {
        public BreakerState GetState(State state, IStateManager manager)
        {
            
            BreakerState breakerState = null;
            switch (state)
            {
                case State.Close:
                    breakerState = new CloseState(manager);
                    break;
                default:
                    throw new NotSupportedException("state");
            }
            
            return breakerState;
        }
    }
}