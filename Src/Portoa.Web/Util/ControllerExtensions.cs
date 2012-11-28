using System;
using System.Linq;
using System.Web.Mvc;
using JetBrains.Annotations;
using Portoa.Util;
using Portoa.Web.Models;
using Portoa.Web.Results;

namespace Portoa.Web.Util {
	public static class ControllerExtensions {
		/// <summary>
		/// Gets all model state errors as a line feed-delimited string
		/// </summary>
		public static string ValidationErrorsToString(this Controller controller) {
			return controller
				.ModelState
				.SelectMany(kvp => kvp.Value.Errors)
				.Implode(error => error.ErrorMessage, "\n");
		}

		/// <summary>
		/// Creates a JSON response if an error occurred, appending all <see cref="Controller.ModelState"/> errors
		/// to the response object
		/// </summary>
		/// <param name="errorMessage">The error message to display to the user, this value cannot be null</param>
		/// <see cref="CreateJsonResponse"/>
		public static object CreateJsonErrorResponse(this Controller controller, [NotNull]string errorMessage) {
			var data = controller
				.ModelState
				.Where(kvp => kvp.Value.Errors.Any())
				.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Implode(error => error.ErrorMessage, ", "));

			return controller.CreateJsonResponse(errorMessage, data);
		}

		/// <summary>
		/// Creates a JSON response from an <c>Exception</c>
		/// </summary>
		/// <see cref="CreateJsonResponse"/>
		/// <seealso cref="CreateJsonErrorResponse(System.Web.Mvc.Controller,string)"/>
		public static object CreateJsonErrorResponse(this Controller controller, [NotNull]Exception exception) {
			return controller.CreateJsonResponse(exception.Message);
		}

		/// <summary>
		/// Creates a JSON response object
		/// </summary>
		/// <param name="errorMessage">The error message to display to the user (<c>null</c> if no error occurred)</param>
		/// <param name="data">Any extra data that needs to be passed to the client</param>
		/// <see cref="JsonResponse"/>
		public static object CreateJsonResponse(this Controller controller, string errorMessage = null, object data = null) {
			return new JsonResponse { Error = errorMessage, Data = data ?? new object() };
		}

		/// <summary>
		/// Sets up the temporary data provider so that it won't barf with Session errors
		/// </summary>
		/// <seealso cref="NoTempDataProvider"/>
		public static Controller DoNotUseTempData(this Controller controller) {
			controller.TempDataProvider = new NoTempDataProvider();
			return controller;
		}

		/// <summary>
		/// Serializes <paramref name="data"/> to a <c>JSON</c> string and returns it as
		/// <c>application/json</c>
		/// </summary>
		/// <param name="errorMessage">The error message to display to the user or <c>null</c> if no error occurred</param>
		/// <param name="data">The data to serialize to a JSON string</param>
		public static ActionResult GetJsonResult(this Controller controller, string errorMessage = null, object data = null) {
			return new InjectableJsonResult { Data = controller.CreateJsonResponse(errorMessage, data) };
		}
	}
}