using System;

namespace Portoa.Logging {
	public class ConsoleLogger : AbstractLogger {
		protected override void Log(object message) {
			Console.WriteLine(message);
		}
	}
}