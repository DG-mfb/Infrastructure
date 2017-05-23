using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kernel.DependancyResolver;
using Kernel.Initialisation;
using Kernel.Reflection;
using Shared.Initialisation;
using Shared.Logging;

namespace ServerInitialisation
{
	public class ServerInitialiser : IServerInitialiser
	{
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
			using (new InformationLogEventWriter())
			{
				var types = ReflectionHelper.GetAllTypes(t => !t.IsAbstract && !t.IsInterface && typeof(IAutoRegister).IsAssignableFrom(t));

				foreach (var t in types)
				{
					dependencyResolver.RegisterType(t, Lifetime.Transient);
				}
			}
		}
	}
}