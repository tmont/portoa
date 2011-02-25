using System.Reflection;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Portoa.Web.Unity.Matching {
	/// <summary>
	/// Matching rule where a member is match if it is assignable from <typeparamref name="T"/>
	/// </summary>
	/// <typeparam name="T">The base class/interface to match against</typeparam>
	public class InstanceOf<T> : IMatchingRule {
		public bool Matches(MethodBase member) {
			return typeof(T).IsAssignableFrom(member.DeclaringType);
		}
	}
}