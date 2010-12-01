using System;

namespace Portoa.Web.Unity {
	public interface IInterceptRule {
		bool Matches(Type typeToIntercept, Type typeOfInstance);
	}
}