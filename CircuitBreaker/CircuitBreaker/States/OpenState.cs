﻿using System;
using System.Threading.Tasks;
using CircuitBreakerInfrastructure;

namespace CircuitBreaker.States
{
    internal class OpenState : BreakerState
    {
        public OpenState()
        {
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