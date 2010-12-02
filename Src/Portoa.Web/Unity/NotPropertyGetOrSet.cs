using System.Reflection;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Portoa.Web.Unity {
	public class NotPropertyGetOrSet : IMatchingRule {
		public bool Matches(MethodBase member) {
			return !(member.IsSpecialName && (member.Name.StartsWith("get_") || member.Name.StartsWith("set_")));
		}
	}
}