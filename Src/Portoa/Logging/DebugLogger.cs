namespace Portoa.Logging {
	public class DebugLogger : AbstractLogger {
		protected override void Log(object message) {
			System.Diagnostics.Debug.WriteLine(message);
		}
	}
}