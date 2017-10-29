using System;
using CircuitBreakerInfrastructure;

namespace CircuitBreaker.ExecutionResults
{
    internal abstract class ExecutionResult : IExecutionResult
    {
        public ExecutionResult(Func<object> result):this(result, null)
        {

        }

        public ExecutionResult(Func<object> result, Exception ex)
        {

        }
        public IBrakerResponse Execute()
        {
            return this.BuildResponse();
        }

        protected abstract IBrakerResponse BuildResponse();
    }
}