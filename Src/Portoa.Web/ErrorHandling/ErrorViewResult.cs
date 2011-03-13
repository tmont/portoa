using System;
using System.Web.Mvc;
using JetBrains.Annotations;

namespace Portoa.Web.ErrorHandling {
	/// <summary>
	/// Represents a view for when an error occurs
	/// </summary>
	public class ErrorViewResult : ViewResult {
		private static readonly Func<Exception, object> defaultModelCreator = exception => new ErrorModel { Exception = exception };

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
		/// an instance of <see cref="ErrorModel" />. The first argument (the exception) can be null.
		/// </summary>
		public Func<Exception, object> ModelCreator { get; set; }

		public override void ExecuteResult(ControllerContext context) {
			ViewData.Model = (ModelCreator ?? defaultModelCreator)(Error ?? context.RouteData.Values["error"] as Exception);

			ViewData["message"] = Message ?? context.RouteData.Values["message"] ?? "An error occurred.";
			base.ExecuteResult(context);
		}
	}
}