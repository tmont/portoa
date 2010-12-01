using System.Collections.Generic;

namespace Portoa.Util {
	public static class CollectionExtensions {
		public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> collectionToAdd) {
			foreach (var item in collectionToAdd) {
				collection.Add(item);
			}
		}
	}
}