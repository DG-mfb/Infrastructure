using System;
using System.Linq;
using CircuitBreakerInfrastructure;
using CircuitBreakerTests.MockData;
using NUnit.Framework;

namespace CircuitBreakerTests
{
    [TestFixture]
    public class CircuitBreakerTests
    {
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