using System.Web.Mvc;

namespace Portoa.Web.Filters {
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