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
		private static readonly SmartCaseConverter caseConverter = new SmartCaseConverter();
		
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

				//don't mess with the query string and/or fragment
				var mark = data.VirtualPath.IndexOf('?');
				var frag = data.VirtualPath.IndexOf('#');
				var length = mark < 0 ? (frag < 0 ? data.VirtualPath.Length : frag) : mark;
				var path = data.VirtualPath.Substring(0, length);
				var queryAndFragment = length < data.VirtualPath.Length ? data.VirtualPath.Substring(length) : string.Empty;

				data.VirtualPath = path
					.Split('/')
					.Select(segment => caseConverter.ConvertTo(segment))
					.Implode(segment => segment, "/") +
					queryAndFragment;
			}

			return data;
		}
	}
}