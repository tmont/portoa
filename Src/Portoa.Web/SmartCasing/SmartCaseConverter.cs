using System.Text;
using JetBrains.Annotations;

namespace Portoa.Web.SmartCasing {
	/// <summary>
	/// Applies smart casing rules to strings
	/// </summary>
	public sealed class SmartCaseConverter {
		/// <summary>
		/// Converts a smart-cased string back to a "dumb" one
		/// </summary>
		/// <param name="value">The smart-cased string to convert</param>
		public string ConvertFrom([NotNull]string value) {
			if (value.Length == 0) {
				return value;
			}

			var builder = new StringBuilder(value.Length);
			foreach (var hyphenatedSegment in value.Split('-')) {
				builder.Append(hyphenatedSegment[0].ToString().ToUpperInvariant() + hyphenatedSegment.Substring(1));
			}

			return builder.ToString();
		}

		/// <summary>
		/// Converts a "dumb" string to a smart-cased one
		/// </summary>
		/// <param name="value">The non-smart-cased string to convert</param>
		public string ConvertTo([NotNull]string value) {
			if (value.Length == 0) {
				return value;
			}

			var builder = new StringBuilder(value.Length * 2);
			foreach (var c in value) {
				if (c >= 'A' && c <= 'Z') {
					builder.Append("-" + c.ToString().ToLowerInvariant());
				} else {
					builder.Append(c.ToString());
				}
			}

			return builder.ToString().TrimStart('-');
		}
	}
}