namespace Portoa.EventLog {
	public abstract class GenericEventLogPropertyHandler<T> : IEventLogPropertyHandler<T> where T : class {
		public abstract object HandleProperty(T propertyValue);

		object IEventLogPropertyHandler.HandleProperty(object propertyValue) {
			return HandleProperty(propertyValue as T);
		}
	}
}