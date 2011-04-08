using System.Web;
using System.Web.Routing;
using Moq;
using NUnit.Framework;
using Portoa.Web.SmartCasing;

namespace Portoa.Web.Tests.SmartCasing {
	[TestFixture]
	public class SmartCasingTests {

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

		[Test]
		public void Should_convert_requestContext_action_value() {
			var innerHandler = new Mock<IRouteHandler>();
			innerHandler.Setup(h => h.GetHttpHandler(It.IsAny<RequestContext>())).Verifiable();
			var handler = new SmartCaseRouteHandler(innerHandler.Object);

			var requestContext = new RequestContext {
				RouteData = new RouteData()
			};

			requestContext.RouteData.Values["action"] = "video-games";

			handler.GetHttpHandler(requestContext);

			Assert.That(requestContext.RouteData.Values["action"], Is.EqualTo("VideoGames"));
			innerHandler.VerifyAll();
		}

		[Test]
		public void Should_replace_capital_letters_hyphens_and_lowercase_letters() {
			Assert.That(SmartCaseConverter.ConvertTo("fooBar"), Is.EqualTo("foo-bar"));
		}

		[Test]
		public void Should_lowercase_first_letter_of_word() {
			Assert.That(SmartCaseConverter.ConvertTo("Foo"), Is.EqualTo("foo"));
		}

		[Test]
		public void Should_capitalize_first_letter() {
			Assert.That(SmartCaseConverter.ConvertFrom("foo"), Is.EqualTo("Foo"));
		}

		[Test]
		public void Should_replace_hyphens_with_capital_letters() {
			Assert.That(SmartCaseConverter.ConvertFrom("foo-bar"), Is.EqualTo("FooBar"));
		}

		[Test]
		public void Should_handle_consecutive_hyphens() {
			Assert.That(SmartCaseConverter.ConvertFrom("foo--bar"), Is.EqualTo("FooBar"));
		}
	}
}