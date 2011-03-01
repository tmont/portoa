using System;

namespace Portoa.Logging {
	/// <summary>
	/// <see cref="ILogger"/> implementation that writes using system diagnostic tool
	/// </summary>
	public class DebugLogger : AbstractLogger {
		protected override void Log(object message) {
			System.Diagnostics.Debug.WriteLine(message);
		}
	}
}