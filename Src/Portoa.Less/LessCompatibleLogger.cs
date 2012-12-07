using JetBrains.Annotations;
using Portoa.Logging.Log4Net;
using dotless.Core.Loggers;
using log4net;
using Portoa.Logging;
using ILogger = dotless.Core.Loggers.ILogger;

namespace Portoa.Less {
	/// <summary>
	/// A logger decorator that is compatible with both log4net and dotLess.
	/// Useful for when your logger is created via an IoC container.
	/// </summary>
	public class LessCompatibleLogger : Log4NetLogger, ILogger {
		public LessCompatibleLogger([NotNull]ILog log4NetLogger) : base(log4NetLogger) { }

		void ILogger.Log(LogLevel level, string message) {
			switch (level) {
				case LogLevel.Info:
					Info(message);
					break;
				case LogLevel.Debug:
					Debug(message);
					break;
				case LogLevel.Warn:
					Warn(message);
					break;
				case LogLevel.Error:
					Error(message);
					break;
			}
		}

		void ILogger.Info(string message) {
			Info(message);
		}

		void ILogger.Info(string message, params object[] args) {
			this.Info(message, args);
		}

		void ILogger.Debug(string message) {
			Debug(message);
		}

		void ILogger.Debug(string message, params object[] args) {
			this.Debug(message, args);
		}

		void ILogger.Warn(string message) {
			Error(message);
		}

		void ILogger.Warn(string message, params object[] args) {
			this.Warn(message, args);
		}

		void ILogger.Error(string message) {
			Error(message);
		}

		void ILogger.Error(string message, params object[] args) {
			this.Error(message, args);
		}
	}
}