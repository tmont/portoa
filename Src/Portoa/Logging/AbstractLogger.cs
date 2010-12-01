namespace Portoa.Logging {
	public abstract class AbstractLogger : ILogger {

		public bool IsDebugEnabled {
			get { return true; }
		}

		public bool IsInfoEnabled {
			get { return true; }
		}

		public bool IsWarnEnabled {
			get { return true; }
		}

		public bool IsErrorEnabled {
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

		protected abstract void Log(object message);
	}
}