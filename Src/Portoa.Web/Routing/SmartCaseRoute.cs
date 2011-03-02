using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using JetBrains.Annotations;

namespace Portoa.Web.Routing {
	/// <summary>
	/// Route that handles casing intelligently. It converts incoming paths to lowercase,
	/// and adds a hyphen before each upper case letter (unless it starts the string). Use
	/// <see cref="RouteExtensions.MapSmartRoute"/> to make use of this class.
	/// </summary>
	public class SmartCaseRoute : Route {
		protected internal SmartCaseRoute(string url, IRouteHandler routeHandler) : base(url, routeHandler) {}

		public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values) {
			var data = base.GetVirtualPath(requestContext, values);

			if (data != null) {
				//convert to lower case, and add a hyphen before each uppercase letter
				//FooBar -> foo-bar
				//fooBar -> foo-bar
				//foo -> foo
				//foobar -> foobar
				//FoOBAr -> fo-o-b-ar

				var urlBuilder = new StringBuilder();
				foreach (var pathSegment in data.VirtualPath.Split('/')) {
					var segmentBuilder = new StringBuilder(pathSegment.Length * 2);
					foreach (var c in pathSegment) {
						if (c >= 'A' && c <= 'Z') {
							segmentBuilder.Append("-" + c.ToString().ToLowerInvariant());
						} else {
							segmentBuilder.Append(c.ToString());
						}
					}

					urlBuilder.Append(segmentBuilder.ToString().TrimStart('-') + "/");
				}

				data.VirtualPath = urlBuilder.ToString().TrimEnd('/');
			}

			return data;
		}
	}

	public static class RouteExtensions {
		/// <summary>
		/// Adds a URL pattern to the route collection utilizing intelligent casing when
		/// deconstructing URLs
		/// </summary>
		/// <param name="name">The name of the route, or null if unnamed</param>
		/// <param name="url">The route's URL pattern</param>
		/// <param name="defaults">Default values (if applicable) for this route</param>
		/// <param name="constraints">Constraints (if applicable) for the route values</param>
		/// <param name="namespaces">Namespaces to search (unused)</param>
		public static void MapSmartRoute(this RouteCollection routes, string name, [NotNull]string url, object defaults = null, object constraints = null, string[] namespaces = null) {
			var route = new SmartCaseRoute(url, new MvcRouteHandler()) {
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