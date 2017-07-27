using System.Collections.Generic;
using System.Reflection;

namespace Kernel.CQRS.MessageHandling
{
    public interface IHandlerFactorySettings
    {
        IEnumerable<Assembly> LimitAssembliesTo { get; }
    }
}