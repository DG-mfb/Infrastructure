using System;
using CircuitBreakerInfrastructure;

namespace CircuitBreaker.ExecutionResults
{
    internal abstract class ExecutionResult : IExecutionResult
    {
        protected Func<object> Result;
        protected Exception Error;
        public ExecutionResult(Func<object> result):this(result, State.Close, null)
        {
        }

        public ExecutionResult(Func<object> result, State state, Exception ex)
        {
            this.Result = result;
            this.Error = ex;
            this.BrakerState = state;
        }

        public State BrakerState { get; }

        public IBrakerResponse Execute()
        {
            return this.BuildResponse();
        }

        protected abstract IBrakerResponse BuildResponse();
    }
}