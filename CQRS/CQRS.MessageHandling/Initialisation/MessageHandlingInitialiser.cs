using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kernel.CQRS.Command;
using Kernel.CQRS.MessageHandling;
using Kernel.DependancyResolver;
using Shared.Initialisation;

namespace CQRS.MessageHandling.Initialisation
{
    class MessageHandlingInitialiser : Initialiser
    {
        public override byte Order
        {
            get { return 0; }
        }

        protected override Task InitialiseInternal(IDependencyResolver dependencyResolver)
        {
            throw new NotImplementedException();
        }

        internal static Func<Type, IHandlerFactory> RegisterHandlerFactories(IDependencyResolver dependencyResolver)
        {
            return t =>
            {
                //var settings = typeof(IArticleBoundContext).IsAssignableFrom(t) ? (IHandlerFactorySettings)dependencyResolver.Resolve<IArticleDispatcherSettings>()
                //: (IHandlerFactorySettings)dependencyResolver.Resolve<IDirectoryDispatcherSettings>();

                //if (typeof(Command).IsAssignableFrom(t))
                //    return new CommandHandlerFactory(dependencyResolver, settings);

                //if (typeof(Event).IsAssignableFrom(t))
                //    return new EventHandlerFactory(dependencyResolver, settings);

                throw new InvalidOperationException($"Unknown type: {t.Name}. The type must inherit Command or Event");
            };
        }
    }
}
