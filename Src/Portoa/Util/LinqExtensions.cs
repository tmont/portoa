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
	}
}