using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CircuitBreaker.BreakerProxy;
using CircuitBreaker.StateManagers;
using CircuitBreakerInfrastructure;
using CircuitBreakerTests.MockData;
using NUnit.Framework;

namespace CircuitBreakerTests
{
    [TestFixture]
    public class CircuitBreakerTests
    {
        [Test]
        public async Task BreakerTest()
        {
            //ARRANGE
            var validator = new BackchannelCertificateValidatorMock(() => true);
            var webClient = new HttpDocumentRetrieverMock(validator);
            var manager = new StateManager(new TimeManager(), new StateProviderMock());
            BreakerProxy.StateProviderFactory(() => manager);
            var breaker = BreakerProxy.Instance;
            var result = String.Empty;
            var executingContext = new BreakerExecutionContext { Action = async() => result = await webClient.GetDocumentAsync("https://dg-mfb/idp/shibboleth", CancellationToken.None) };
            //ACT
            var response = await breaker.Execute(executingContext);
            //ASSERT
        }

        [Test]
        public async Task BreakerTest1()
        {
            //ARRANGE
            var validator = new BackchannelCertificateValidatorMock(() => true);
            var webClient = new HttpDocumentRetrieverMock(validator);
            var manager = new StateManager(new TimeManager(), new StateProviderMock());
            BreakerProxy.StateProviderFactory(() => manager);
            var breaker = BreakerProxy.Instance;
            
            var executingContext = new BreakerExecutionContext { Action = () => webClient.GetDocumentAsync("https://dg-mfb/idp/shibboleth", CancellationToken.None) };
            //ACT
            var response = await breaker.Execute(executingContext);
            //ASSERT
        }

        [Test]
        public async Task BreakerTest2()
        {
            //ARRANGE
            var validator = new BackchannelCertificateValidatorMock(() => true);
            var webClient = new HttpDocumentRetrieverMock(validator);
            var manager = new StateManager(new TimeManager(), new StateProviderMock());
            BreakerProxy.StateProviderFactory(() => manager);
            var breaker = BreakerProxy.Instance;

            var executingContext = new BreakerExecutionContext { Action = () => webClient.GetDocumentAsync("https://dg-mfb/idp/shibboleth", CancellationToken.None) };
            //ACT
            var response = await breaker.Execute(executingContext);
            response = await breaker.Execute(executingContext);
            Thread.Sleep(8000);
            response = await breaker.Execute(executingContext);
            //ASSERT
        }

        //[Test]
        //public void TestMethod1()
        //{
        //    var strategies = new FailedOperationStrategy[] { new PingFailedOperationStrategy(), new TimeoutFailedOperationStrategy() };
        //    Action<FailedOperationContext> seed = (c) => 
        //    {
        //    };
        //    var chain = strategies.Aggregate(seed, (f, next) => new Action<FailedOperationContext>(c =>  next.Apply(c, f)));
        //    var context = new FailedOperationContext();
        //    chain(context);
        //}
    }
}