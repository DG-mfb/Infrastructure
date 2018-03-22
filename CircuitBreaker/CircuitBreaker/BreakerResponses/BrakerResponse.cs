using System;
using CircuitBreakerInfrastructure;
using Kernel.Extensions;

namespace CircuitBreaker.BreakerResponses
{
    public class BrakerResponse : IBrakerResponse
    {
        public BrakerResponse(object result, State state)
            :this(result, state, null)
        {
        }

        public BrakerResponse(object result, State state, Exception ex)
        {
            this.Exception = ex;
            this.Result = result;
            this.BrakerState = state;
        }
        public object Result { get; }

        public Type ResultType
        {
            get
            {
                return this.Result == null ? null : Result.GetType();
            }
        }

        public State BrakerState { get; }

        public string ProjectedError
        {
            get
            {
                return this.Exception == null ? null : ExceptionExtensions.BuildExceptionStringRecursively(this.Exception);
            }
        }

        public Exception Exception { get; }
    }
}