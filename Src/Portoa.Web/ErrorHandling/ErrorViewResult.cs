using System;
using System.Net;
using System.Web.Mvc;
using Portoa.Web.Filters;

namespace Portoa.Web.ErrorHandling {
	public class ErrorViewResult : ViewResult, IStatusOverridable {
		public HttpStatusCode StatusCode { get; set; }

		public string Message { get; set; }
		public Exception Error { get; set; }

		/// <summary>
		/// Sets the HTTP status code on the response object before
		/// calling base.ExecuteResult()
		/// </summary>
		public override void ExecuteResult(ControllerContext context) {
			context.HttpContext.Response.StatusCode = (int)StatusCode;
			ViewData.Model = new ErrorModel {
				Exception = Error ?? context.RouteData.Values["error"] as Exception
			};

			ViewData["message"] = Message ?? (context.RouteData.Values["message"] ?? "An error occurred.");
			base.ExecuteResult(context);
		}

	}
}