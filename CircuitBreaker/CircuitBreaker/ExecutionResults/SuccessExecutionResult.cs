using System;
using CircuitBreaker.BreakerResponses;
using CircuitBreakerInfrastructure;

namespace CircuitBreaker.ExecutionResults
{
    internal class SuccessExecutionResult : ExecutionResult
    {
        public SuccessExecutionResult(Func<object> result) : base(result)
        {
        }

        protected override IBrakerResponse BuildResponse()
        {
            return new BrakerResponse(base.Result(), base.BrakerState);
        }
    }
}