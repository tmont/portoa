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

		[Test]
		public void Should_be_assignable_from_open_generic_type_to_concrete_open_generic_type() {
			Assert.That(typeof(Foo<>).IsAssignableToGenericType(typeof(IFoo<>)), Is.True);
		}

		[Test]
		public void Should_be_assignable_from_open_generic_type_to_generic_interface_type() {
			Assert.That(typeof(IFoo<int>).IsAssignableToGenericType(typeof(IFoo<>)), Is.True);
		}

		[Test]
		public void Should_be_assignable_from_open_generic_type_to_itself() {
			Assert.That(typeof(IFoo<>).IsAssignableToGenericType(typeof(IFoo<>)), Is.True);
		}

		[Test]
		public void Should_be_assignable_from_open_generic_type_to_concrete_generic_type() {
			Assert.That(typeof(Foo<int>).IsAssignableToGenericType(typeof(IFoo<>)), Is.True);
		}

		[Test]
		public void Should_be_assignable_from_open_generic_type_to_nongeneric_concrete_type() {
			Assert.That(typeof(Bar).IsAssignableToGenericType(typeof(IFoo<>)), Is.True);
		}

		public interface IFoo<T> {}
		public class Foo<T> : IFoo<T> { }
		public class Bar : IFoo<int> { }

		public enum Foo {
			Foo,
			Bar,
			[Description("lol!")]
			Baz,
			Bat
		}
	}
}