using System;

namespace Portoa.EventLog {
	public sealed class ActivatorEventLogPropertyHandlerFactory : IEventLogPropertyHandlerFactory {
		public IEventLogPropertyHandler Create(Type type) {
			return (IEventLogPropertyHandler)Activator.CreateInstance(type ?? typeof(DefaultEventLogPropertyHandler));
		}
	}
}