using System;
using System.Web;
using System.Web.Routing;
using Moq;
using NUnit.Framework;
using Portoa.Web.SmartCasing;

namespace Portoa.Web.Tests.SmartCasing {
	[TestFixture]
	public class SmartCasingTests {

		[Test]
		public void das() {
			var bar = "foo";
			var mark = bar.IndexOf('?');
			var frag = bar.IndexOf('#');
			var length = mark < 0 ? (frag < 0 ? bar.Length : frag) : mark;
			Console.WriteLine(bar.Substring(0, length) + (length < bar.Length ? bar.Substring(length) : string.Empty));
		}

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
		public void Should_replace_capital_letters_hyphens_and_lowercase_letters() {
			var converter = new SmartCaseConverter();

			Assert.That(converter.ConvertTo("fooBar"), Is.EqualTo("foo-bar"));
		}

		[Test]
		public void Should_lowercase_first_letter_of_word() {
			var converter = new SmartCaseConverter();

			Assert.That(converter.ConvertTo("Foo"), Is.EqualTo("foo"));
		}

		[Test]
		public void Should_capitalize_first_letter() {
			var converter = new SmartCaseConverter();

			Assert.That(converter.ConvertFrom("foo"), Is.EqualTo("Foo"));
		}

		[Test]
		public void Should_replace_hyphens_with_capital_letters() {
			var converter = new SmartCaseConverter();

			Assert.That(converter.ConvertFrom("foo-bar"), Is.EqualTo("FooBar"));
		}
	}
}