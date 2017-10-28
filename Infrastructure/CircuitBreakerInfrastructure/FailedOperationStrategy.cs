using System;

namespace CircuitBreakerInfrastructure
{
    public abstract class FailedOperationStrategy
    {
        public abstract void Apply(FailedOperationContext context, Action<FailedOperationContext> next);
    }
}