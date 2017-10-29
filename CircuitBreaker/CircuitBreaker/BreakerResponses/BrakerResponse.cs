using System;
using CircuitBreakerInfrastructure;

namespace CircuitBreaker.BreakerResponses
{
    public class BrakerResponse : IBrakerResponse
    {
        public BrakerResponse(object result, State state)
        {
            this.Result = result;
            this.BrakerState = state;
        }
        public object Result { get; }

        public Type ResultType { get { return Result.GetType(); } }

        public State BrakerState { get; }

        public string ProjectedError
        {
            get;
        }

        public Exception Exception { get; }
    }
}