using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Portoa.Web.Unity {
	public static class CallHandlerExtensions {

		/// <summary>
		/// Gets the MethodInfo associated with the actual object instance
		/// in a CallHandler's method invocation
		/// </summary>
		public static MethodInfo GetInstanceMethodInfo(this IMethodInvocation input) {
			return input
				.Target
				.GetType()
				.GetMethod(
					input.MethodBase.Name,
					input
						.MethodBase
						.GetParameters()
						.Select(param => param.ParameterType)
						.ToArray()
				);
		}
	}
}