using System;
using CircuitBreaker.BreakerResponses;
using CircuitBreakerInfrastructure;

namespace CircuitBreaker.ExecutionResults
{
    internal class FailedExecutionResult : ExecutionResult
    {
        public FailedExecutionResult(Func<object> result, Exception e) : base(result, e)
        {
        }

        protected override IBrakerResponse BuildResponse()
        {
            return new BrakerResponse();
        }
    }
}