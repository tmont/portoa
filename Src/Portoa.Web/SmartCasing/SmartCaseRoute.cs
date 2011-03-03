using System.Linq;
using System.Web.Routing;
using Portoa.Util;

namespace Portoa.Web.SmartCasing {
	/// <summary>
	/// Route that handles casing intelligently. It converts incoming paths to lowercase,
	/// and adds a hyphen before each upper case letter (unless it starts the string). Use
	/// <see cref="RouteExtensions.MapSmartRoute"/> to make use of this class.
	/// </summary>
	/// <seealso cref="SmartCaseViewEngine"/>
	public class SmartCaseRoute : Route {
		private static readonly SmartCasingConverter casingConverter = new SmartCasingConverter();
		
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

				data.VirtualPath = data
					.VirtualPath
					.Split('/')
					.Select(segment => casingConverter.ConvertTo(segment))
					.Implode(segment => segment, "/");
			}

			return data;
		}
	}
}