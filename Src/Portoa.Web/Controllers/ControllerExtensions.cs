﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Portoa.Util;
using Portoa.Web.Models;
using Portoa.Web.Util;

namespace Portoa.Web.Controllers {
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
		/// Creates a JSON response if an error occurred
		/// </summary>
		/// <param name="errorMessage">The error message to display to the user</param>
		/// <see cref="CreateJsonResponse"/>
		public static object CreateJsonErrorResponse(this Controller controller, string errorMessage) {
			var data = new Dictionary<string, string>();

			foreach (var kvp in controller.ModelState.Where(kvp => kvp.Value.Errors.Any())) {
				data[kvp.Key] = kvp.Value.Errors.Implode(error => error.ErrorMessage, ", ");
			}

			return controller.CreateJsonResponse(errorMessage, data);
		}

		/// <summary>
		/// Creates a JSON response object
		/// </summary>
		/// <param name="errorMessage">The error message to display to the user</param>
		/// <param name="data">Any data that needs to be passed to the client</param>
		public static object CreateJsonResponse(this Controller controller, string errorMessage = null, IDictionary<string, string> data = null) {
			return new JsonReturn { Error = errorMessage, Data = data ?? new Dictionary<string, string>() };
		}

		/// <summary>
		/// Sets up the temporary data provider so that it won't barf with Session errors
		/// </summary>
		public static Controller DoNotUseTempData(this Controller controller) {
			controller.TempDataProvider = new NoTempDataProvider();
			return controller;
		}
	}
}