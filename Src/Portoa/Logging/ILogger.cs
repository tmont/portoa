namespace Portoa.Logging {
	public interface ILogger {
		void Debug(object data);
		void Info(object data);
		void Warn(object data);
		void Error(object data);

		bool IsDebugEnabled { get; }
		bool IsInfoEnabled { get; }
		bool IsWarnEnabled { get; }
		bool IsErrorEnabled { get; }
	}
}