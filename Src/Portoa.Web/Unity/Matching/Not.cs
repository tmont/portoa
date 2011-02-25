using System.Reflection;
using JetBrains.Annotations;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Portoa.Web.Unity.Matching {

	/// <summary>
	/// Negates a matching rule
	/// </summary>
	/// <typeparam name="T">The type of matching rule to negate</typeparam>
	public class Not<T> : Not where T : IMatchingRule, new() {
		public Not() : base(new T()) { }
	}

	/// <summary>
	/// Negates a matching rule
	/// </summary>
	public class Not : IMatchingRule {
		private readonly IMatchingRule ruleToNegate;

		/// <param name="ruleToNegate">The matching rule to negate</param>
		public Not([NotNull]IMatchingRule ruleToNegate) {
			this.ruleToNegate = ruleToNegate;
		}

		public bool Matches(MethodBase member) {
			return !ruleToNegate.Matches(member);
		}
	}

}