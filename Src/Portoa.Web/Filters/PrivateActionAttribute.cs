using System;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Portoa.Web.Filters {
	/// <summary>
	/// Disallows public access to a controller's action. This is basically a more useful version of 
	/// <see cref="ChildActionOnlyAttribute"/>. When the action is accessed publicly, an <see cref="HttpException"/> 
	/// with a 404 status code is raised.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public sealed class PrivateActionAttribute : FilterAttribute, IResultFilter {
		public void OnResultExecuting(ResultExecutingContext filterContext) {
			if (filterContext.IsChildAction) {
				//okay to invoke via RenderAction()
				return;
			}

			throw new HttpException((int)HttpStatusCode.NotFound, "This action is private and cannot be invoked publicly");
		}

		public void OnResultExecuted(ResultExecutedContext filterContext) { }
	}
}