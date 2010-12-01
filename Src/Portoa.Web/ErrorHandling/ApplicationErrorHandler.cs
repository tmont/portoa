using System;
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
		private readonly ILogger logger;
		private readonly HttpContextBase context;

		public ApplicationErrorHandler(ILogger logger, HttpContextBase context) {
			this.logger = logger;
			this.context = context;
		}

		/// <summary>
		/// Handles global application errors by invoking the specified error controller
		/// </summary>
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
					return "NotFound";
				case 403:
					return "Forbidden";
				default:
					return "Unknown";
			}
		}

		private static RouteData GetRouteData(Exception exception) {
			var routeData = new RouteData();
			routeData.Values["controller"] = "Error";

			var httpException = exception as HttpException;
			routeData.Values["action"] = (httpException != null) ? GetErrorAction(httpException.GetHttpCode()) : "Unknown";
			routeData.Values["error"] = exception;
			routeData.Values["message"] = exception.Message;
			return routeData;
		}
	}
}