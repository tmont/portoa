using System.Reflection;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Portoa.Web.Unity.Matching {
	/// <summary>
	/// Matching rule that matches compiler-generated methods for property
	/// getters and setters
	/// </summary>
	public class PropertyGetOrSet : IMatchingRule {
		public bool Matches(MethodBase member) {
			return member.IsSpecialName && (member.Name.StartsWith("get_") || member.Name.StartsWith("set_"));
		}
	}
}