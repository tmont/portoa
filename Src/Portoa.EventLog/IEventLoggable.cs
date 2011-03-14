using System.Collections.Generic;
using System.Reflection;

namespace Portoa.EventLog {
	public interface IEventLoggable {
		IDictionary<MemberInfo, object> OriginalData { get; }
	}
}