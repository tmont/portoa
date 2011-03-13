using System.Web.Mvc;
using System.Web.Routing;
using JetBrains.Annotations;

namespace Portoa.Web.SmartCasing {
	public static class RouteExtensions {
		/// <summary>
		/// Adds a URL pattern to the route collection utilizing intelligent casing when
		/// deconstructing URLs. Make sure you add the <see cref="SmartCaseViewEngine"/> to
		/// your <see cref="ViewEngineCollection"/>.
		/// </summary>
		/// <param name="name">The name of the route, or null if unnamed</param>
		/// <param name="url">The route's URL pattern</param>
		/// <param name="defaults">Default values (if applicable) for this route</param>
		/// <param name="constraints">Constraints (if applicable) for the route values</param>
		/// <param name="namespaces">Namespaces to search (unused)</param>
		public static void MapSmartRoute(this RouteCollection routes,string name, [NotNull]string url, object defaults = null, object constraints = null, string[] namespaces = null) {
			var route = new SmartCaseRoute(url, new SmartCaseRouteHandler(new MvcRouteHandler())) {
				Defaults = new RouteValueDictionary(defaults),
				Constraints = new RouteValueDictionary(constraints),
				DataTokens = new RouteValueDictionary()
			};

			if (namespaces != null && namespaces.Length > 0) {
				route.DataTokens["Namespaces"] = namespaces;
			}

			routes.Add(name, route);
		}
	}
}