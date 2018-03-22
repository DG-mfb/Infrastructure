using System;
using System.Threading.Tasks;
using CQRS.MessageHandling.Factories;
using CQRS.MessageHandling.Invocation;
using Kernel.CQRS.MessageHandling;
using Kernel.DependancyResolver;
using Shared.Initialisation;

namespace CQRS.MessageHandling.Initialisation
{
    public class MessageHandlingInitialiser : Initialiser
    {
        public override byte Order
        {
            get { return 0; }
        }

        protected override Task InitialiseInternal(IDependencyResolver dependencyResolver)
        {
            dependencyResolver.RegisterType<HandlerResolver>(Lifetime.Transient);
            dependencyResolver.RegisterType<HandlerInvoker>(Lifetime.Transient);
            return Task.CompletedTask;
        }

        internal static Func<Type, IHandlerResolver> RegisterHandlerFactories(IDependencyResolver dependencyResolver)
        {
            return t =>
            {
                throw new InvalidOperationException($"Unknown type: {t.Name}. The type must inherit Command or Event");
            };
        }
    }
}