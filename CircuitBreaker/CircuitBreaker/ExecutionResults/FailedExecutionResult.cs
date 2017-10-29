using System;
using CircuitBreaker.BreakerResponses;
using CircuitBreakerInfrastructure;

namespace CircuitBreaker.ExecutionResults
{
    internal class FailedExecutionResult : ExecutionResult
    {
        public FailedExecutionResult(Func<object> result, State state, Exception e) : base(result, state, e)
        {
        }

        protected override IBrakerResponse BuildResponse()
        {
            return new BrakerResponse(base.Result(), base.BrakerState);
        }
    }
}