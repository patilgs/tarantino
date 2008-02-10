using Tarantino.Commons.Core.Services.Performance.Impl;
using StructureMap;

namespace Tarantino.Commons.Core.Services.Performance.Impl
{
	[Pluggable(ServiceKeys.Default)]
	public class CacheManager : ICacheManager
	{
		public void Set<T>(object key, T objectToCache)
		{
			Cache.Set(key, objectToCache);
		}

		public T Get<T>(object key)
		{
			return Cache.Get<T>(key);
		}

		public void Clear()
		{
			Cache.Clear();
		}

		public bool Has(object key)
		{
			return Cache.Has(key);
		}
	}
}