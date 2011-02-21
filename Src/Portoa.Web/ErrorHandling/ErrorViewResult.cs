using System;
using System.Net;
using System.Web.Mvc;
using JetBrains.Annotations;
using Portoa.Web.Filters;

namespace Portoa.Web.ErrorHandling {
	/// <summary>
	/// Represents a view for when an error occurs
	/// </summary>
	public class ErrorViewResult : ViewResult, IStatusOverridable {

		public ErrorViewResult() {
			ModelCreator = exception => new ErrorModel { Exception = exception };
		}

		public HttpStatusCode StatusCode { get; set; }

		/// <summary>
		/// Gets or sets the error message
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Gets or sets the error that occurred
		/// </summary>
		[CanBeNull]
		public Exception Error { get; set; }

		/// <summary>
		/// Gets or sets the delegate to create the error model. By default it creates
		/// an instance of <see cref="ErrorModel">ErrorModel</see>.
		/// </summary>
		public Func<Exception, object> ModelCreator { get; set; }

		/// <summary>
		/// Sets the HTTP status code on the response object before
		/// calling <c>base.ExecuteResult()</c>
		/// </summary>
		public override void ExecuteResult(ControllerContext context) {
			context.HttpContext.Response.StatusCode = (int)StatusCode;
			ViewData.Model = ModelCreator(Error ?? context.RouteData.Values["error"] as Exception);

			ViewData["message"] = Message ?? context.RouteData.Values["message"] ?? "An error occurred.";
			base.ExecuteResult(context);
		}

	}
}