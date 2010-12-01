using System.Text;

namespace Portoa.Util {
	public static class StringBuilderExtensions {
		public static string ClearAndReturn(this StringBuilder buffer) {
			var text = buffer.ToString();
			buffer.Clear();
			return text;
		}
	}
}