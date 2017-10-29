using System;
using System.Threading;
using System.Threading.Tasks;
using CircuitBreaker.ExecutionResults;
using CircuitBreakerInfrastructure;

namespace CircuitBreaker.States
{
    internal class HalfOpenState : BreakerState
    {
        private int _successCount;
        private readonly SemaphoreSlim _lock;
        public HalfOpenState(IStateManager stateManager) : base(stateManager)
        {
            this._lock = new SemaphoreSlim(1);
        }

        public override State State
        {
            get
            {
                return State.HalfOpen;
            }
        }

        protected override async Task<IExecutionResult> ExecuteInternal(BreakerExecutionContext executionContext)
        {
            await this._lock.WaitAsync();
            try
            {
                var t = executionContext.Action();

                t.Wait();
                this._successCount++;
                if (this._successCount > 0)
                    this.StateManager.Close();
                return new SuccessExecutionResult(base.GetResultFactory(t));
            }
            catch(Exception e)
            {
                return await this.Trip(e, executionContext);
            }
            finally
            {
                this._lock.Release();
            }
        }

        protected override Task<IExecutionResult> Trip(Exception e, BreakerExecutionContext executionContext)
        {
            this.StateManager.Open();
            return Task.FromResult<IExecutionResult>(new FailedExecutionResult(() => null, this.StateManager.CurrentState.State, e));
        }
    }
}