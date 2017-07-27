using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Kernel.CQRS.MessageHandling;
using Kernel.DependancyResolver;
using Kernel.Extensions;
using Kernel.Reflection;

namespace CQRS.MessageHandling.Factories
{
    internal abstract class HandlerFactory : IHandlerFactory
    {
        private readonly IDependencyResolver _resolver;
        private readonly IHandlerFactorySettings _factorySettings;

        //internal static readonly Func<Type, IMessageDispatcherSettings, bool> _normalisedHandlerCondition = new Func<Type, IMessageDispatcherSettings, bool>((t, s) => t.Assembly.GetName().Name == s.NormalisedAssemblyName);
        //internal static readonly Func<Type, IMessageDispatcherSettings, bool> _subscriberHandlerCondition = new Func<Type, IMessageDispatcherSettings, bool>((t, s) =>
        //s.LimitAssembliesTo.Any(x => x.Equals(t.Assembly.GetName().Name, StringComparison.Ordinal)) && !_normalisedHandlerCondition(t, s));

        public HandlerFactory(IDependencyResolver resolver, IHandlerFactorySettings factorySettings)
        {
            _resolver = resolver;
            _factorySettings = factorySettings;
        }

        public ICollection<object> GetAllHandlersFor(Type targetType)
        {
            return GetHandlersFor(targetType, (t, s) => true);
        }

        public ICollection<object> GetHandlersFor(Type targetType, Func<Type, IHandlerFactorySettings, bool> filter)
        {
            var handlerType = BuildHandlerType(targetType);
            return GetHandlersInternal(handlerType, filter);
        }

        //public ICollection<object> GetSubscriberHandlersFor(Type targetType)
        //{
        //    return this.GetHandlersFor(targetType, HandlerFactory._subscriberHandlerCondition);
        //}

        //public ICollection<object> GetNormilisedHandlersFor(Type targetType)
        //{
        //    return this.GetHandlersFor(targetType, HandlerFactory._normalisedHandlerCondition);
        //}

        protected virtual ICollection<object> GetHandlersInternal(Type handlerType, Func<Type, IHandlerFactorySettings, bool> filter)
        {
            var handlers = ResolveHandlers(handlerType, filter);
            var filteredHandlers = ApplyHandlerFilter(handlers);
            return filteredHandlers;
        }

        protected abstract Type BuildHandlerType(Type type);
        protected abstract ICollection<object> ApplyHandlerFilter(ICollection<object> handlers);

        protected virtual ICollection<object> ResolveHandlers(Type handlerType, Func<Type, IHandlerFactorySettings, bool> filter)
        {
            //object handler;
            var handlers = this._resolver.ResolveAll(handlerType)
                .Where(h => filter(h.GetType(), this._factorySettings))
                .ToList();

            if (handlers.Count > 0)
                return handlers;

            handlers = TryResolveFromAssemblies(handlerType, filter)
                .ToList();

            if (handlers.Count == 0)
                throw new InvalidOperationException($"No command handler of type: {handlerType.FullName} found.");

            return handlers;
        }

        private ICollection<object> TryResolveFromAssemblies(Type handlerType, Func<Type, IHandlerFactorySettings, bool> filter)
        {
            var filterAssembles = AssemblyScanner.ScannableAssemblies
                .Select(a => new { Name = a.GetName().Name, a })
                .Join(this._factorySettings.LimitAssembliesTo, key => key.Name, keyIn => keyIn.FullName, (a, b) => a.a);
            var implementors = ReflectionHelper.GetAllTypes(filterAssembles, t => !t.IsAbstract && !t.IsInterface && TypeExtensions.IsAssignableToGenericType(t, handlerType) && filter(t, this._factorySettings));

            var root = new List<object>();
            var instances = implementors.Aggregate(root, (c, next) => { c.Add(this.CreateInstance(next)); return c; });
            return instances;
        }

        private object CreateInstance(Type type)
        {
            var ctors = type.GetConstructors();
            var ctor = ctors.OrderByDescending(c => c.GetParameters().Length).First();
            var parameters = ctor.GetParameters();
            var pars = new List<ParameterExpression>();
            var resolvedParams = new List<object>();
            foreach (var p in parameters)
            {
                object parInstance;
                var canResolve = _resolver.TryResolve(p.ParameterType, out parInstance);
                if (!canResolve)
                    throw new InvalidOperationException($"Cannot resolve dependency for type: {type.Name}. Dependency type: {p.ParameterType.Name}");
                resolvedParams.Add(parInstance);
                pars.Add(Expression.Parameter(p.ParameterType));
            }
            var ctorExpression = Expression.New(ctor, pars);
            var lambda = Expression.Lambda(ctorExpression, pars);
            var instance = lambda.Compile().DynamicInvoke(resolvedParams.ToArray());
            return instance;
        }
    }
}