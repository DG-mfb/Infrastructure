using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Kernel.Extensions;

namespace CQRS.MessageHandling.Invocation
{
    internal class HandlerFactory
    {
        private static readonly ConcurrentDictionary<Tuple<Type, Type>, Func<object, object[], Task>> MessageHandlerDelegatesCache = new ConcurrentDictionary<Tuple<Type, Type>, Func<object, object[], Task>>();

        public static Func<object, object[], Task> BuildMessageHandlerDelegate(Type handlerType, Type commandType)
        {
            return HandlerFactory.MessageHandlerDelegatesCache.GetOrAdd(new Tuple<Type, Type>(handlerType, commandType), t => HandlerFactory.BuildMessageHandlerDelegateInternal(handlerType, commandType));
        }

        private static Func<object, object[], Task> BuildMessageHandlerDelegateInternal(Type targetType, Type commandType)
        {
            return TypeExtensions.GetAsyncInvoker(targetType, "Handle", commandType);
            //var method = targetType.GetMethods().Single(c => c.Name == "Handle");
            //var targetParam = Expression.Parameter(typeof(object));
            //var eventParam = Expression.Parameter(typeof(object));

            //var handlerConvert = Expression.Convert(targetParam, targetType);
            //var commandConvert = Expression.Convert(eventParam, commandype);

            //var callExpression = Expression.Call(handlerConvert, method, commandConvert);
            //var lambda = Expression.Lambda<Func<object, object, Task>>(callExpression, targetParam, eventParam);
            //var compiledLambda = lambda.Compile();
            //return compiledLambda;
        }
    }
}