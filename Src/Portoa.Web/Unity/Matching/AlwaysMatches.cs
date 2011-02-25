using System.Reflection;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Portoa.Web.Unity.Matching {
	/// <summary>
	/// A matching rule that always matches
	/// </summary>
	public class AlwaysMatches : IMatchingRule {
		public bool Matches(MethodBase member) {
			return true;
		}
	}
}