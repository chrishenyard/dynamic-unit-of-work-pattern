using System;

namespace Region.Caching {
	public class Cache : ICache {
		private System.Web.Caching.Cache _cache = System.Web.HttpRuntime.Cache; 

		public T Get<T>(string key) {
			T cacheItem = (T)_cache.Get(key);
			return cacheItem;
		}

		public void Set<T>(string key, T value, int expiration) {
			_cache.Add(
				key, 
				value, 
				null, 
				DateTime.Now.AddMinutes(expiration), 
				System.Web.Caching.Cache.NoSlidingExpiration, 
				System.Web.Caching.CacheItemPriority.Normal, 
				null);
		}
	}
}
