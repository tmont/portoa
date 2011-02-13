using System.Web.Mvc;

namespace Portoa.Web.Filters {
	/// <summary>
	/// If the result implements <see cref="IStatusOverridable"/>, then the HTTP status
	/// code on the <c>Response</c> will be set according to the value of
	/// <see cref="IStatusOverridable.StatusCode"/>
	/// </summary>
	public class OverrideStatusCodeFilter : IResultFilter {
		public void OnResultExecuting(ResultExecutingContext filterContext) { }

		public void OnResultExecuted(ResultExecutedContext filterContext) {
			var statusResult = filterContext.Result as IStatusOverridable;
			if (statusResult == null) {
				return;
			}

			filterContext.HttpContext.Response.StatusCode = (int)statusResult.StatusCode;
		}
	}
}