namespace Portoa.EventLog {
	public interface IEventLogPropertyHandler<in T> : IEventLogPropertyHandler {
		object HandleProperty(T propertyValue);
	}

	public interface IEventLogPropertyHandler {
		object HandleProperty(object propertyValue);
	}
}