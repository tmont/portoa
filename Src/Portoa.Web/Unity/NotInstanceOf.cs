using System;
using System.Reflection;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Portoa.Web.Unity {
	public class NotInstanceOf<T> : IInterceptRule, IMatchingRule {
		public bool Matches(Type typeToIntercept, Type typeOfInstance) {
			return !typeof(T).IsAssignableFrom(typeOfInstance);
		}

		public bool Matches(MethodBase member) {
			return !typeof(T).IsAssignableFrom(member.DeclaringType);
		}
	}
}