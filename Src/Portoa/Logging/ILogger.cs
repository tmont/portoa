namespace Portoa.Logging {
	/// <summary>
	/// Exposes an interface for logging
	/// </summary>
	public interface ILogger {
		/// <summary>
		/// Logs a debug message
		/// </summary>
		void Debug(object data);
		/// <summary>
		/// Logs an informational message
		/// </summary>
		void Info(object data);

		/// <summary>
		/// Logs a warning message
		/// </summary>
		void Warn(object data);

		/// <summary>
		/// Logs an error message
		/// </summary>
		void Error(object data);

		/// <summary>
		/// Gets whether or not <c>Debug</c> messages should be logged
		/// </summary>
		bool IsDebugEnabled { get; }

		/// <summary>
		/// Gets whether or not <c>Info</c> messages should be logged
		/// </summary>
		bool IsInfoEnabled { get; }

		/// <summary>
		/// Gets whether or not <c>Warn</c> messages should be logged
		/// </summary>
		bool IsWarnEnabled { get; }

		/// <summary>
		/// Gets whether or not <c>Error</c> messages should be logged
		/// </summary>
		bool IsErrorEnabled { get; }
	}
}