using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Portoa.Css {
	/// <summary>
	/// Stores cached items in a local <see cref="ConcurrentDictionary{TKey,TValue}"/>.
	/// This class is thread safe-ish. There are no locks, so items could be updated
	/// in one thread while be reading in a different thread, so be aware.
	/// </summary>
	public class InMemoryCacheStrategy : ICssCacheStrategy {
		private readonly IDictionary<string, CssCacheItem> lessMap = new ConcurrentDictionary<string, CssCacheItem>();

		public CssCacheItem Get(string key) {
			if (!lessMap.ContainsKey(key)) {
				return null;
			}

			var item = lessMap[key];
			try {
				if (item.Ttl != TimeSpan.MaxValue && item.Created.Add(item.Ttl) <= DateTime.UtcNow) {
					//expired, remove from cache
					Delete(key);
					return null;
				}
			}
			catch (ArgumentOutOfRangeException) {
				//if this happens, the TTL was high enough that it caused an overflow.
				//in this case we just assume that it's not expired.
			}

			return item;
		}

		public void Set(string key, string compiled, CssCacheOptions options = null) {
			options = options ?? new CssCacheOptions();
			lessMap[key] = new CssCacheItem {
				Created = DateTime.UtcNow,
				Css = compiled,
				Ttl = options.Ttl
			};
		}

		public void Delete(string key) {
			if (!lessMap.ContainsKey(key)) {
				return;
			}

			lessMap.Remove(key);
		}
	}
}