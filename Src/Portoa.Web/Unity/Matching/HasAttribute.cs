using System;
using System.Reflection;
using Microsoft.Practices.Unity.InterceptionExtension;
using Portoa.Util;

namespace Portoa.Web.Unity.Matching {
	/// <summary>
	/// Matching rule that matches when a method is annotated with an attribute
	/// </summary>
	/// <typeparam name="T">The attribute to match against</typeparam>
	public class HasAttribute<T> : IMatchingRule where T : Attribute {
		public bool Matches(MethodBase member) {
			return member.HasAttribute<T>();
		}
	}
}