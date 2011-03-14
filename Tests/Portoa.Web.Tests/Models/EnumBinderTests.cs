using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Web.Mvc;
using NUnit.Framework;
using Portoa.Web.Models;

namespace Portoa.Web.Tests.Models {
	[TestFixture]
	public class EnumBinderTests {
		[Test]
		public void Should_bind_enum_value_by_name() {
			var collection = new NameValueCollection { { "Foo", "Bar" } };

			var modelBindingContext = new ModelBindingContext {
				ValueProvider = new NameValueCollectionValueProvider(collection, CultureInfo.InvariantCulture),
				ModelName = "Foo",
			};

			var value = new EnumModelBinder<Foo>().BindModel(new ControllerContext(), modelBindingContext);
			Assert.That(value, Is.EqualTo(Foo.Bar));
		}

		[Test]
		public void Should_fall_back_to_default_enum_value_if_model_name_is_not_present() {
			var modelBindingContext = new ModelBindingContext {
				ValueProvider = new NameValueCollectionValueProvider(new NameValueCollection(), CultureInfo.InvariantCulture),
				ModelName = "Foo",
			};

			var value = new EnumModelBinder<Foo>().BindModel(new ControllerContext(), modelBindingContext);
			Assert.That(value, Is.EqualTo(Foo.None));
		}

		[Test]
		public void Should_fall_back_to_default_enum_value_if_value_does_not_exist_on_enum() {
			var modelBindingContext = new ModelBindingContext {
				ValueProvider = new NameValueCollectionValueProvider(new NameValueCollection { { "Foo", "Does not exist" } }, CultureInfo.InvariantCulture),
				ModelName = "Foo",
			};

			var value = new EnumModelBinder<Foo>().BindModel(new ControllerContext(), modelBindingContext);
			Assert.That(value, Is.EqualTo(Foo.None));
		}

		[Test]
		public void Should_fall_back_to_injected_default_enum_value_if_value_is_not_present() {
			var modelBindingContext = new ModelBindingContext {
				ValueProvider = new NameValueCollectionValueProvider(new NameValueCollection(), CultureInfo.InvariantCulture),
				ModelName = "Foo",
			};

			var value = new EnumModelBinder<Foo>().BindModel(new ControllerContext(), modelBindingContext);
			Assert.That(value, Is.EqualTo(Foo.None));
		}

		[Test]
		public void Should_bitwise_or_multiple_enum_values() {
			var modelBindingContext = new ModelBindingContext {
				ValueProvider = new NameValueCollectionValueProvider(new NameValueCollection { { "Foo", "Bar" }, { "Foo", "Baz" } }, CultureInfo.InvariantCulture),
				ModelName = "Foo"
			};

			var value = new FlagEnumModelBinder<Foo>().BindModel(new ControllerContext(), modelBindingContext);
			Assert.That(value, Is.EqualTo(Foo.Bar | Foo.Baz));
		}

		[Test]
		public void Should_fall_back_to_default_value_if_values_are_not_present() {
			var modelBindingContext = new ModelBindingContext {
				ValueProvider = new NameValueCollectionValueProvider(new NameValueCollection(), CultureInfo.InvariantCulture),
				ModelName = "Foo"
			};

			var value = new FlagEnumModelBinder<Foo>().BindModel(new ControllerContext(), modelBindingContext);
			Assert.That(value, Is.EqualTo(Foo.None));
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void Should_not_allow_instantiation_if_type_is_not_a_flagged_enum() {
			new FlagEnumModelBinder<object>();
		}

		[Flags]
		public enum Foo {
			None = 0,
			Bar = 1,
			Bat = 2,
			Baz = 4,
			Quux = 8
		}
	}
}