namespace Portoa.EventLog {
	public sealed class DefaultEventLogPropertyHandler : IEventLogPropertyHandler {
		public object HandleProperty(object propertyValue) {
			return propertyValue;
		}
	}
}