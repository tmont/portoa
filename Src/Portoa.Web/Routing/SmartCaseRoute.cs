using System.Text;
using System.Web.Routing;

namespace Portoa.Web.Routing {
	/// <summary>
	/// Route that handles casing intelligently. It converts incoming paths to lowercase,
	/// and adds a hyphen before each upper case letter (unless it starts the string). Use
	/// <see cref="RouteExtensions.MapSmartRoute"/> to make use of this class.
	/// </summary>
	/// <seealso cref="SmartCaseViewEngine"/>
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
}