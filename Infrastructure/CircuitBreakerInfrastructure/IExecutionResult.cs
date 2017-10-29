namespace CircuitBreakerInfrastructure
{
    public interface IExecutionResult
    {
        State BrakerState { get; }
        IBrakerResponse Execute();
    }
}