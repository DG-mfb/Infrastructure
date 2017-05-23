namespace Kernel.Cache
{
	using System;
	using Kernel.Initialisation;

	/// <summary>
	/// Interface ICachePopulator
	/// </summary>
	public interface ICachePopulator : IAutoRegisterAsTransient, IDisposable
	{
		/// <summary>
		/// Gets the cache key.
		/// </summary>
		/// <value>The cache key.</value>
		string CacheKey { get; }

		/// <summary>
		/// Populates this instance.
		/// </summary>
		void Populate();
	}
}