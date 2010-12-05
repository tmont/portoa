using System;
using NUnit.Framework;
using Portoa.Web.Unity;

namespace Portoa.Web.Tests {

	[TestFixture]
	public class NonMagicInjectionPropertyTest {

		internal sealed class Foo {
			public string Bar { get; set; }
		}

		[Test]
		public void Should_correctly_parse_expression() {
			new NonMagicInjectionProperty<Foo>(foo => foo.Bar);
			new NonMagicInjectionProperty<Foo>(Bar => Bar.Bar, "foo");
			new NonMagicInjectionProperty<Foo, string>(foo => foo.Bar);
			new NonMagicInjectionProperty<Foo, string>(Bar => Bar.Bar, "foo");
		}

		[Test]
		[ExpectedException(typeof(ArgumentException), ExpectedMessage = "Expected lambda expression like: foo => foo.Bar, where Bar is the name of the property to be injected")]
		public void Should_force_use_of_specified_parameter1() {
			new NonMagicInjectionProperty<Foo>(foo => new Foo().Bar);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException), ExpectedMessage = "Expected lambda expression like: foo => foo.Bar, where Bar is the name of the property to be injected")]
		public void Should_force_use_of_specified_parameter2() {
			var bar = new Foo();
			new NonMagicInjectionProperty<Foo>(foo => bar.Bar);
		}

		[Test]
		[ExpectedException(typeof(ArgumentException), ExpectedMessage = "Expected lambda expression like: foo => foo.Bar, where Bar is the name of the property to be injected")]
		public void Should_check_for_usage_of_generic_type() {
			new NonMagicInjectionProperty<Foo>(foo => new string('f', 1).Length);
		}
	}
}
