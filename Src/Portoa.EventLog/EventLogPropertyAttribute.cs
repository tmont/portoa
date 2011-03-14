using System;
using Portoa.Util;

namespace Portoa.EventLog {
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public class EventLogPropertyAttribute : Attribute {
		public EventLogPropertyAttribute() : this(null) { }

		public EventLogPropertyAttribute(Type itemHandler) {
			if (itemHandler != null && !typeof(IEventLogPropertyHandler).IsAssignableFrom(itemHandler)) {
				throw new ArgumentException(string.Format("Given type must be assignable from {0}", typeof(IEventLogPropertyHandler).GetFriendlyName()), "itemHandler");
			}

			ItemHandler = itemHandler;
		}

		public Type ItemHandler { get; private set; }
	}
}