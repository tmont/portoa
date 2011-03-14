using System;

namespace Portoa.EventLog {
	public interface IEventLogPropertyHandlerFactory {
		IEventLogPropertyHandler Create(Type type);
	}
}