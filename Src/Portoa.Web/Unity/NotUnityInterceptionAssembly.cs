using System;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Portoa.Web.Unity {
	public class NotUnityInterceptionAssembly : IInterceptRule {
		public bool Matches(Type typeToIntercept, Type typeOfInstance) {
			return !typeToIntercept.Assembly.Equals(typeof(Interception).Assembly);
		}
	}
}