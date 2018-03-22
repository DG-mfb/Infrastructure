using System.Threading.Tasks;
using CQRS.Infrastructure.Messaging;
using CQRS.Infrastructure.Transport;
using CQRS.InMemoryTransport.Serializers;
using Kernel.CQRS.MessageHandling;
using Kernel.CQRS.Transport;
using Kernel.DependancyResolver;
using Shared.Initialisation;

namespace CQRS.InMemoryTransport.Initialisation
{
    public class InMemoryTransportInitialiser : Initialiser
    {
        public override byte Order { get { return 0; } }

        protected override Task InitialiseInternal(IDependencyResolver dependencyResolver)
        {
            dependencyResolver.RegisterType<InMemorySerializer>(Lifetime.Transient);
            dependencyResolver.RegisterFactory<ITransportConfiguration>(() => new TransportConfiguration(), Lifetime.Transient);
            dependencyResolver.RegisterFactory<ITranspontDispatcher>(() =>
            {
                var transport = dependencyResolver.Resolve<InMemoryQueueTransport>();
                var manager = dependencyResolver.Resolve<TransportManager>();
                var serializer = dependencyResolver.Resolve<IMessageSerialiser>();
                var listener = new MessageListener(() => dependencyResolver.Resolve<IHandlerResolver>(), () => dependencyResolver.Resolve<IHandlerInvoker>(), serializer);
                listener.AttachTo(manager);
                manager.Initialise();
                manager.Start();
                return new TransportDispatcher(manager, serializer);
            }, Lifetime.Singleton);
            return Task.CompletedTask;
        }
    }
}