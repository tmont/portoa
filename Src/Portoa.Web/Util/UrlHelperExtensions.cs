using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Portoa.Web.Util {
	public static class UrlHelperExtensions {
		public static string Action<T>(this UrlHelper urlHelper, Expression<Func<T, object>> actionExpression, object routeValues = null) where T : IController {
			var expression = actionExpression.Body as MethodCallExpression;
			
			if (expression == null) {
				throw new ArgumentException("actionExpression must be a MethodCallExpression");
			}

			var parameter = expression.Object as ParameterExpression;
			if (parameter == null || parameter.Type != typeof(T)) {
				throw new ArgumentException("Expected expression like: controller => controller.ActionMethod()");
			}

			var controllerClass = parameter.Type.Name;
			var controllerName = controllerClass.Substring(0, controllerClass.LastIndexOf("Controller"));
			var actionName = expression.Method.Name;
			return urlHelper.Action(actionName, controllerName, routeValues);
		}

	}
}