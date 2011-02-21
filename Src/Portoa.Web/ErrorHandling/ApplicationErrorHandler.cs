﻿using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Portoa.Logging;
using Portoa.Web.Controllers;

namespace Portoa.Web.ErrorHandling {
	/// <summary>
	/// Handles global application-wide errors that don't get caught by
	/// normal means
	/// </summary>
	public class ApplicationErrorHandler {
		private static readonly string unknownAction;
		private static readonly string notFoundAction;
		private static readonly string forbiddenAction;

		private readonly ILogger logger;
		private readonly HttpContextBase context;

		/// <param name="logger">The system's logger</param>
		/// <param name="context">The current <c>HttpContext</c></param>
		public ApplicationErrorHandler(ILogger logger, HttpContextBase context) {
			this.logger = logger;
			this.context = context;
		}

		static ApplicationErrorHandler() {
			//ths hideousness is just to enable refactoring, and so i don't have to hardcode strings
			unknownAction = ((MethodCallExpression)((Expression<Func<IErrorController, object>>)(controller => controller.Unknown())).Body).Method.Name;
			notFoundAction = ((MethodCallExpression)((Expression<Func<IErrorController, object>>)(controller => controller.NotFound())).Body).Method.Name;
			forbiddenAction = ((MethodCallExpression)((Expression<Func<IErrorController, object>>)(controller => controller.Forbidden())).Body).Method.Name;
		}

		/// <summary>
		/// Handles global application errors by invoking the specified error controller
		/// </summary>
		/// <param name="exception">The exception that caused the error</param>
		/// <param name="errorController">The controller to use handle errors</param>
		public virtual void HandleError(Exception exception, IErrorController errorController) {
			logger.Error(exception);
			var routeData = GetRouteData(exception);

			var requestContext = new RequestContext(context, routeData);

			if (errorController is Controller) {
				((Controller)errorController).DoNotUseTempData();
			}

			errorController.Execute(requestContext);
		}

		private static string GetErrorAction(int httpStatusCode) {
			switch (httpStatusCode) {
				case 404:
					return notFoundAction;
				case 403:
					return forbiddenAction;
				default:
					return unknownAction;
			}
		}

		private static RouteData GetRouteData(Exception exception) {
			var routeData = new RouteData();
			routeData.Values["controller"] = "Error";

			var httpException = exception as HttpException;
			routeData.Values["action"] = httpException != null ? GetErrorAction(httpException.GetHttpCode()) : unknownAction;
			routeData.Values["error"] = exception;
			routeData.Values["message"] = exception.Message;
			return routeData;
		}
	}
}