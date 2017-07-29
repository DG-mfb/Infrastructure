using System.Threading;
using CQRS.InMemoryTransport;
using CQRS.InMemoryTransportTests.MockData.Listeners;
using NUnit.Framework;

namespace CQRS.InMemoryTransportTests.InMemoryTransportTests
{
    [TestFixture]
    internal class ListenerTests
    {
        [Test]
        public void EnqueueMessageTest()
        {
            //ARRANGE
            var transport = new InMemoryQueueTransport();
            var manager = new TransportManager(transport);
            var message = new byte[] { 0, 1, 2 };
            
            byte[] messageReceived = null;
            
            var listener = new MessageListener1(m => messageReceived = m);
            //ACT
            listener.AttachTo(manager);
            transport.Start();
            manager.EnqueueMessage(message);
            Thread.Sleep(500);
            //ASSERT
            
            Assert.AreEqual(message, messageReceived);
        }
    }
}