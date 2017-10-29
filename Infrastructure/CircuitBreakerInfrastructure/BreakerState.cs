using System;
using System.Threading.Tasks;
using Kernel.Extensions;

namespace CircuitBreakerInfrastructure
{
    public abstract class BreakerState
    {
        protected DateTimeOffset EnterOn;
        protected DateTimeOffset ExitedOn;
        protected IExecutionResult LastResult;
        protected IStateManager StateManager;

        protected BreakerState(IStateManager stateManager)
        {
            this.StateManager = stateManager;
        }

        public abstract State State { get; }
        
        public virtual Task Enter()
        {
            this.EnterOn = DateTimeOffset.Now;
            return Task.CompletedTask;
        }
        public virtual Task Exit()
        {
            this.ExitedOn = DateTimeOffset.Now;
            return Task.CompletedTask;
        }
        public Task<IExecutionResult> Execute(BreakerExecutionContext executionContext)
        {
            try
            {
                return this.ExecuteInternal(executionContext);
            }
            catch(Exception e)
            {
                return Trip(e, executionContext);
            }
        }

        protected Func<object> GetResultFactory(Task task)
        {
            return () =>
            {
                var hasResult = TypeExtensions.IsAssignableToGenericType(task.GetType(), typeof(Task<>));
                if (!hasResult)
                    return null;
                var del = Kernel.Reflection.Extensions.TypeExtensions.GetInstancePropertyDelegate<object>(task.GetType(), "Result");
                var res = del(task);
                return res;
            };
        }

        protected abstract Task<IExecutionResult> Trip(Exception e, BreakerExecutionContext executionContext);
       

        protected abstract Task<IExecutionResult> ExecuteInternal(BreakerExecutionContext executionContext);
    }
}