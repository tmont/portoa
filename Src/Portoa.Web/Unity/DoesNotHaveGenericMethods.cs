using System;
using System.Linq;

namespace Portoa.Web.Unity {
	public class DoesNotHaveGenericMethods : IInterceptRule {
		public bool Matches(Type typeToIntercept, Type typeOfInstance) {
			return typeOfInstance.GetMethods().Any();
		}
	}
}