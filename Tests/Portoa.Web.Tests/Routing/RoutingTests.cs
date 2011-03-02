using System.Web;
using System.Web.Routing;
using Moq;
using NUnit.Framework;
using Portoa.Web.Routing;

namespace Portoa.Web.Tests.Routing {
	[TestFixture]
	public class RoutingTests {
		[Test]
		public void Should_apply_smart_casing_to_routes() {
			var routes = new RouteCollection();
			routes.MapSmartRoute("foo", "{foo}/{bar}");

			Assert.That(routes, Has.Count.EqualTo(1));

			var requestContext = new RequestContext(new Mock<HttpContextBase>().Object, new RouteData());
			var path = routes["foo"].GetVirtualPath(requestContext, new RouteValueDictionary(new { foo = "Lol", bar = "MyAction" }));
			Assert.That(path, Is.Not.Null);
			Assert.That(path.VirtualPath, Is.EqualTo("lol/my-action"));
		}
	}
}