using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Portoa.Util;

namespace Portoa.Logging {
	[DebuggerNonUserCode]
	public static class LoggingExtensions {
		/// <summary>
		/// Determines if this object should be logged
		/// </summary>
		public static bool IsLoggable(this ICustomAttributeProvider attributeProvider) {
			return !attributeProvider.GetAttributes<DoNotLogAttribute>().Any();
		}

	}
}