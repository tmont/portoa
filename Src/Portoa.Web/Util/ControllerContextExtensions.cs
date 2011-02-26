using System.Web.Mvc;

namespace Portoa.Web.Util {
	public static class ControllerContextExtensions {
		/// <summary>
		/// Gets a value from the request and casts it to the specified type
		/// </summary>
		public static T GetFromRequest<T>(this ControllerContext controllerContext, string key) {
			return controllerContext.HttpContext.Request.Get<T>(key);
		}

		/// <summary>
		/// Gets a value from the request
		/// </summary>
		public static string GetFromRequest(this ControllerContext controllerContext, string key) {
			return controllerContext.HttpContext.Request.Get<string>(key);
		}

		/// <summary>
		/// Adds an error to the model state
		/// </summary>
		public static void AddModelError(this ControllerContext controllerContext, string key, string errorMessage) {
			controllerContext.Controller.ViewData.ModelState.AddModelError(key, errorMessage);
		}
	}
}