using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Portoa.Css {
	/// <summary>
	/// Stores cached items in a local <see cref="ConcurrentDictionary{TKey,TValue}"/>.
	/// </summary>
	public class InMemoryCacheStrategy : ICssCacheStrategy {
		private readonly IDictionary<string, CssCacheItem> lessMap = new ConcurrentDictionary<string, CssCacheItem>();

		public CssCacheItem Get(string key) {
			if (!lessMap.ContainsKey(key)) {
				return null;
			}

			var item = lessMap[key];
			if (item.Created.Add(item.Ttl) > DateTime.UtcNow) {
				//expired, remove from cache
				Delete(key);
				return null;
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