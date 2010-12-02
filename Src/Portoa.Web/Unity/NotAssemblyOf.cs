using System;

namespace Portoa.Web.Unity {
	public class NotAssemblyOf<T> : IInterceptRule {
		public bool Matches(Type typeToIntercept, Type typeOfInstance) {
			return typeToIntercept.Assembly != typeof(T).Assembly;
		}
	}

}