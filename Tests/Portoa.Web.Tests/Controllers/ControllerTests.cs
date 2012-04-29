using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MvcTestingHelpers;
using NUnit.Framework;
using Portoa.Web.Testing;
using Microsoft.Practices.Unity;

namespace Portoa.Web.Tests.Controllers {
	public class ControllerTests : ControllerTest<MyController> {

		[Test]
		public void Should_execute_action() {
			Assert.That(ExecuteAction(c => c.DoStuff()).As<ContentResult>().Content, Is.EqualTo("foo"));
		}

		[Test]
		public void Should_properly_resolve_controller() {
			Assert.That(Controller, Is.InstanceOf<MyController>());
			Assert.That(Controller.ControllerContext, Is.Not.Null);
			Assert.That(Controller.ControllerContext, Has.Property("Controller").EqualTo(Controller));
			Assert.That(Controller.ControllerContext, Has.Property("RouteData").EqualTo(Container.Resolve<RouteData>()));
			Assert.That(Controller.ControllerContext, Has.Property("HttpContext").EqualTo(Container.Resolve<HttpContextBase>()));
		}
	}

	public class MyController : Controller {
		public ActionResult DoStuff() {
			return new ContentResult {
				Content = "foo"
			};
		}
	}
}
