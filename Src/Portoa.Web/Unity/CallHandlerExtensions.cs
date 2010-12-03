using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Portoa.Web.Unity {
	[DebuggerNonUserCode]
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

		/// <summary>
		/// LINQifies an annoying IParameterCollection
		/// </summary>
		public static IEnumerable<ParameterInfo> GetParameters(this IParameterCollection parameterCollection) {
			for (var i = 0; i < parameterCollection.Count; i++) {
				yield return parameterCollection.GetParameterInfo(i);
			}
		}
	}
}