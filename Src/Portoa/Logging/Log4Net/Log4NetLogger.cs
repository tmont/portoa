using JetBrains.Annotations;
using log4net;
using System.Diagnostics;

namespace Portoa.Logging.Log4Net {
	
	/// <summary>
	/// log4net implementation of <see cref="ILogger"/>
	/// </summary>
	[DebuggerNonUserCode]
	public class Log4NetLogger : ILogger {
		private readonly ILog log4NetLogger;

		public Log4NetLogger([NotNull]ILog log4NetLogger) {
			this.log4NetLogger = log4NetLogger;
		}

		public void Debug(object data) {
			log4NetLogger.Debug(data);
		}

		public void Info(object data) {
			log4NetLogger.Info(data);
		}

		public void Warn(object data) {
			log4NetLogger.Warn(data);
		}

		public void Error(object data) {
			log4NetLogger.Error(data);
		}

		public bool IsDebugEnabled {
			get { return log4NetLogger.IsDebugEnabled; }
		}

		public bool IsInfoEnabled {
			get { return log4NetLogger.IsInfoEnabled; }
		}

		public bool IsWarnEnabled {
			get { return log4NetLogger.IsWarnEnabled; }
		}

		public bool IsErrorEnabled {
			get { return log4NetLogger.IsErrorEnabled; }
		}
	}
}