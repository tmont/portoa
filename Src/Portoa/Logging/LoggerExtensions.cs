using System;
using System.Globalization;

namespace Portoa.Logging {
	public static class LoggerExtensions {
		/// <summary>
		/// Logs a formatted <c>Debug</c> message
		/// </summary>
		/// <see cref="ILogger.Debug"/>
		public static void Debug(this ILogger logger, string messageFormat, params object[] args) {
			logger.Debug(string.Format(CultureInfo.InvariantCulture, messageFormat, args));
		}

		/// <summary>
		/// Logs a formatted <c>Info</c> message
		/// </summary>
		/// <see cref="ILogger.Info"/>
		public static void Info(this ILogger logger, string message, params object[] args) {
			logger.Info(string.Format(CultureInfo.InvariantCulture, message, args));
		}

		/// <summary>
		/// Logs a formatted <c>Warn</c> message
		/// </summary>
		/// <see cref="ILogger.Warn"/>
		public static void Warn(this ILogger logger, string message, params object[] args) {
			logger.Warn(string.Format(CultureInfo.InvariantCulture, message, args));
		}

		/// <summary>
		/// Logs an exception using <see cref="ILogger.Debug"/>
		/// </summary>
		/// <see cref="ILogger.Debug"/>
		public static void Exception(this ILogger logger, Exception e) {
			logger.Debug(e.Message);
			logger.Debug(e.StackTrace);
		}
	}
}