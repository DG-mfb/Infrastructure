using System;
using System.Threading.Tasks;
using CircuitBreakerInfrastructure;

namespace CircuitBreaker.BreakerProxy
{
    internal class BreakerProxy : IBreakerProxy
    {
        static private Func<IStateManager> _stateManagerFactory = () => null;
        private static BreakerProxy _instance;
        public static void StateProviderFactory(Func<IStateManager> stateManagerFactory)
        {
            BreakerProxy._stateManagerFactory = stateManagerFactory;
        }
        public static BreakerProxy Instance
        {
            get
            {
                if (BreakerProxy._instance == null)
                    BreakerProxy._instance = new BreakerProxy(BreakerProxy._stateManagerFactory());
                return BreakerProxy._instance;
            }
        }

        public IStateManager StateManager
        {
            get
            {
                return this._stateManager;
            }
        }
        
        private readonly IStateManager _stateManager;
        
        private BreakerProxy(IStateManager stateManager)
        {
            this._stateManager = stateManager;
           
        }
        
        public async Task<IBrakerResponse> Execute(BreakerExecutionContext executionContext)
        {
            var result = await this._stateManager.Execute(executionContext);
            return result.Execute(this);
        }
    }
}