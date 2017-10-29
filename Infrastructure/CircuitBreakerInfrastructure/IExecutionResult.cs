namespace CircuitBreakerInfrastructure
{
    public interface IExecutionResult
    {
        IBrakerResponse Execute();
    }
}