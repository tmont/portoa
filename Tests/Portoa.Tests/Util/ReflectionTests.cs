using NUnit.Framework;
using Portoa.Util;
using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace Portoa.Tests.Util {
	[TestFixture]
	public class ReflectionTests {
		[Test]
		public void Should_get_attributes_from_enum() {
			Assert.That(Foo.Baz.HasAttribute<DescriptionAttribute>(), Is.True);

			var attributes = Foo.Baz.GetAttributes<DescriptionAttribute>();
			Assert.That(attributes, Has.Length.EqualTo(1));
			Assert.That(attributes[0].Description, Is.EqualTo("lol!"));
		}

		public enum Foo {
			Foo,
			Bar,
			[Description("lol!")]
			Baz,
			Bat
		}
	}
}