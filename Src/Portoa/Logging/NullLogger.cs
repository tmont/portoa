namespace Portoa.Logging {
	/// <summary>
	/// Logger that does nothing
	/// </summary>
	public sealed class NullLogger : AbstractLogger {
		protected override void Log(object message) { }
	}
}