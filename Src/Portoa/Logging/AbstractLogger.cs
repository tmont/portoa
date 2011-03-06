namespace Portoa.Logging {
	/// <summary>
	/// Empty implementation of <see cref="ILogger"/>
	/// </summary>
	public abstract class AbstractLogger : ILogger {
		public virtual bool IsDebugEnabled {
			get { return true; }
		}

		public virtual bool IsInfoEnabled {
			get { return true; }
		}

		public virtual bool IsWarnEnabled {
			get { return true; }
		}

		public virtual bool IsErrorEnabled {
			get { return true; }
		}

		public void Debug(object message) {
			Log(message);
		}

		public void Info(object message) {
			Log(message);
		}

		public void Warn(object message) {
			Log(message);
		}

		public void Error(object message) {
			Log(message);
		}

		/// <summary>
		/// Logs the specified message
		/// </summary>
		/// <param name="message">The message to log</param>
		protected abstract void Log(object message);
	}
}