namespace CircuitBreakerInfrastructure
{
    public interface IStateProvider
    {
        BreakerState GetState(State state, IStateManager manager);
    }
}