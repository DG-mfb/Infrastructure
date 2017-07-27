using System;
using System.Collections.Generic;

namespace Kernel.CQRS.MessageHandling
{
    public interface IHandlerFactory
    {
        ICollection<object> GetAllHandlersFor(Type targetType);
        
        ICollection<object> GetHandlersFor(Type targetType, Func<Type, IHandlerFactorySettings, bool> filter);
    }
}
