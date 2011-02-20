using System.Collections.Generic;

namespace Portoa.Util {
	public static class CollectionExtensions {
		/// <summary>
		/// Iterates over the <paramref name="collectionToAdd"/> and adds each item
		/// to the <paramref name="source"/> collection
		/// </summary>
		public static void AddRange<T>(this ICollection<T> source, IEnumerable<T> collectionToAdd) {
			foreach (var item in collectionToAdd) {
				source.Add(item);
			}
		}
	}
}