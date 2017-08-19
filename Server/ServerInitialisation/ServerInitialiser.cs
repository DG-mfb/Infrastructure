using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AspNet.EntityFramework.IdentityProvider.Managers;
using Kernel.DependancyResolver;
using Kernel.Initialisation;
using Kernel.Logging;
using Kernel.Reflection;
using Microsoft.AspNet.Identity.Owin.Provider.Initialisation;
using Shared.Initialisation;

namespace ServerInitialisation
{
    public class ServerInitialiser : IServerInitialiser
	{
        private IEnumerable<Assembly> AssemblyToAdd
        {
            get
            {
                yield return typeof(ApplicationUserManager).Assembly;
                yield return typeof(OwinIdentityInitialiser).Assembly;
                //return Enumerable.Empty<Assembly>();
            }
        }

        public async Task Initialise(IDependencyResolver dependencyResolver)
		{
			await this.Initialise(dependencyResolver, i => true);
		}

		public async Task Initialise(IDependencyResolver dependencyResolver, Func<Initialiser, bool> condition)
		{
			this.DiscoverAndRegisterTypes(dependencyResolver);
			var initialisers = this.GetInitialisers();
			await this.InitialiseAsync(initialisers, dependencyResolver, condition);
		}

		/// <summary>
		/// Runs all initialisers in parallel
		/// </summary>
		/// <param name="initialisers"></param>
		private async Task InitialiseAsync(IEnumerable<Initialiser> initialisers, IDependencyResolver dependencyResolver, Func<Initialiser, bool> condition)
		{
			var exceptions = new ConcurrentQueue<Exception>();
			//Aggregate all exeptions and throw 
			foreach (var x in initialisers)
			{
				if (!condition(x))
					continue;

				using (new InformationLogEventWriter(String.Format("Initialiser {0}", x.GetType().Name)))
				{
					try
					{
						await x.Initialise(dependencyResolver);
					}
					catch (Exception ex)
					{
						LoggerManager.WriteExceptionToEventLog(ex);
						exceptions.Enqueue(ex);
					}
				}
			};

			if (exceptions.Count > 0)
				throw new AggregateException(exceptions);
		}

		/// <summary>
		/// Discovers and instansiates all initialisers
		/// </summary>
		/// <returns></returns>
		private IEnumerable<Initialiser> GetInitialisers()
		{
			return
				ReflectionHelper
				.GetAllTypes
				(
					x =>
					(
						!x.IsAbstract &&
						typeof(Initialiser).IsAssignableFrom(x)
					)
				)
				.Select(x => Activator.CreateInstance(x) as Initialiser)
				.OrderBy(x => x.Order);
		}

		/// <summary>
		/// Discovers and register types.
		/// </summary>
        private void DiscoverAndRegisterTypes(IDependencyResolver dependencyResolver)
        {
            var scannableAssemblies = AssemblyScanner.ScannableAssemblies
                 .Union(this.AssemblyToAdd);

            var typeToRegister = ReflectionHelper.GetAllTypes(scannableAssemblies, t => !t.IsAbstract && !t.IsInterface && typeof(IAutoRegister).IsAssignableFrom(t));

            //get all transient type and register them
            var transientTypes = typeToRegister.Where(t => typeof(IAutoRegisterAsTransient).IsAssignableFrom(t));
            foreach (var t in transientTypes)
            {
                dependencyResolver.RegisterType(t, Lifetime.Transient);
            }

            //get all transient type and register them
            var singletonTypes = typeToRegister.Where(t => typeof(IAutoRegisterAsSingleton).IsAssignableFrom(t));
            foreach (var t in singletonTypes)
            {
                dependencyResolver.RegisterType(t, Lifetime.Singleton);
            }
        }
    }
}