using System.Web;
using System.Web.Routing;
using JetBrains.Annotations;

namespace Portoa.Web.SmartCasing {
	/// <summary>
	/// <see cref="IRouteHandler"/> decorator that uses <see cref="SmartCaseConverter"/>
	/// to modify the action name before invoking the action
	/// </summary>
	/// <seealso cref="SmartCaseConverter.ConvertTo"/>
	public sealed class SmartCaseRouteHandler : IRouteHandler {
		private readonly IRouteHandler routeHandler;

		/// <param name="routeHandler">The route handler to decorate</param>
		public SmartCaseRouteHandler([NotNull]IRouteHandler routeHandler) {
			this.routeHandler = routeHandler;
		}

		public IHttpHandler GetHttpHandler(RequestContext requestContext) {
			var actionName = requestContext.RouteData.Values["action"] ?? string.Empty;
			requestContext.RouteData.Values["action"] = SmartCaseConverter.ConvertTo(actionName.ToString());

			return routeHandler.GetHttpHandler(requestContext);
		}
	}
}