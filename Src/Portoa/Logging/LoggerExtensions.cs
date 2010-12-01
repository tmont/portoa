using System;
using System.Globalization;

namespace Portoa.Logging {
	public static class LoggerExtensions {

		public static void Debug(this ILogger logger, string message, params object[] args) {
			logger.Debug(string.Format(CultureInfo.InvariantCulture, message, args));
		}

		public static void Info(this ILogger logger, string message, params object[] args) {
			logger.Info(string.Format(CultureInfo.InvariantCulture, message, args));
		}

		public static void Warn(this ILogger logger, string message, params object[] args) {
			logger.Warn(string.Format(CultureInfo.InvariantCulture, message, args));
		}

		public static void Error(this ILogger logger, Exception e) {
			logger.Debug(e.Message);
		}
	}
}