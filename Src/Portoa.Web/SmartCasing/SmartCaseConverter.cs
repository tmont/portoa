using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace Portoa.Web.SmartCasing {
	/// <summary>
	/// Applies smart casing rules to strings
	/// </summary>
	public static class SmartCaseConverter {
		//can't just have one cache because the conversion isn't always reversible (e.g. f---oo -> FOo but FOo -> f-oo)
		private static readonly IDictionary<string, string> fromCache = new Dictionary<string, string>();
		private static readonly IDictionary<string, string> toCache = new Dictionary<string, string>();

		/// <summary>
		/// Converts a smart-cased string back to a "dumb" one
		/// </summary>
		/// <param name="value">The smart-cased string to convert</param>
		public static string ConvertFrom([NotNull]string value) {
			if (value.Length == 0) {
				return value;
			}

			if (!fromCache.Values.Contains(value)) {
				var builder = new StringBuilder(value.Length);
				foreach (var hyphenatedSegment in value.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries)) {
					builder.Append(hyphenatedSegment[0].ToString().ToUpperInvariant());
					if (hyphenatedSegment.Length > 1) {
						builder.Append(hyphenatedSegment.Substring(1));
					}
				}

				fromCache[value] = builder.ToString();
			}

			return fromCache[value];
		}

		/// <summary>
		/// Converts a "dumb" string to a smart-cased one
		/// </summary>
		/// <param name="value">The non-smart-cased string to convert</param>
		public static string ConvertTo([NotNull]string value) {
			if (value.Length == 0) {
				return value;
			}

			if (!toCache.ContainsKey(value)) {
				var builder = new StringBuilder(value.Length * 2);
				foreach (var c in value) {
					if (c >= 'A' && c <= 'Z') {
						builder.Append("-" + c.ToString().ToLowerInvariant());
					} else {
						builder.Append(c.ToString());
					}
				}

				toCache[value] = builder.ToString().TrimStart('-');
			}

			return toCache[value];
		}
	}
}