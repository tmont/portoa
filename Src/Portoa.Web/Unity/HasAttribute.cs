using System;
using System.Reflection;
using Microsoft.Practices.Unity.InterceptionExtension;
using Portoa.Util;

namespace Portoa.Web.Unity {
	public class HasAttribute<T> : IMatchingRule where T : Attribute {
		public bool Matches(MethodBase member) {
			return member.HasAttribute<T>();
		}
	}
}