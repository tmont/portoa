using System;

namespace Portoa.Logging {
	/// <summary>
	/// <see cref="ILogger"/> implementation that writes to the <c>Console</c>
	/// </summary>
	public class ConsoleLogger : AbstractLogger {
		protected override void Log(object message) {
			Console.WriteLine(message);
		}
	}
}