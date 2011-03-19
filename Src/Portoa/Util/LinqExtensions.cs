using System;
using System.Collections.Generic;
using System.Linq;

namespace Portoa.Util {
	public static class LinqExtensions {
		/// <summary>
		/// Implodes an enumeration given a selector function and a separator
		/// </summary>
		public static string Implode<T>(this IEnumerable<T> source, Func<T, string> selector, string separator) {
			return string.Join(separator, source.Select(selector).ToArray());
		}

		/// <summary>
		/// Performs the specified action on each item in the collection. The action will execute immediately.
		/// Use <c cref="WalkDeferred{T}">WalkDeferred</c> if deferred execution is needed.
		/// </summary>
		/// <seealso cref="WalkDeferred{T}"/>
		public static IEnumerable<T> Walk<T>(this IEnumerable<T> collection, Action<T> action) {
			foreach (var item in collection) {
				action(item);
			}

			return collection;
		}

		/// <summary>
		/// Performs the specified action on each item in the collection. The action will not execute until the collection
		/// is enumerated. Use <c cref="Walk{T}">Walk</c> if deferred execution is not needed.
		/// </summary>
		/// <seealso cref="Walk{T}"/>
		public static IEnumerable<T> WalkDeferred<T>(this IEnumerable<T> collection, Action<T> action) {
			foreach (var item in collection) {
				action(item);
				yield return item;
			}
		}
	}
}