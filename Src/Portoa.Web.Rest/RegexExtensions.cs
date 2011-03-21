using System.Text.RegularExpressions;

namespace Portoa.Web.Rest {
	public static class RegexExtensions {
		public static bool Like(this string value, string pattern) {
			return Regex.IsMatch(value, pattern);
		}
	}
}