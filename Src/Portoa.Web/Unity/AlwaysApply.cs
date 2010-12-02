using System.Reflection;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Portoa.Web.Unity {
	public class AlwaysApply : IMatchingRule {
		public bool Matches(MethodBase member) {
			return true;
		}
	}
}