using System;

namespace Portoa.Util {
	public static class DateTimeExtensions {
		private static readonly DateTime unixOrigin = new DateTime(1970, 1, 1, 0, 0, 0);

		/// <summary>
		/// Converts a Unix timestamp to a <see cref="DateTime"/>
		/// </summary>
		public static DateTime AsUnixTimestamp(this int timestamp) {
			return unixOrigin.AddSeconds(timestamp);
		}

		/// <summary>
		/// Converts a <see cref="DateTime"/> to a Unix timestamp (number of seconds since
		/// 1970-01-01)
		/// </summary>
		public static int ToUnixTimestamp(this DateTime date) {
			return (int)Math.Round((date - unixOrigin).TotalSeconds);
		}
	}
}