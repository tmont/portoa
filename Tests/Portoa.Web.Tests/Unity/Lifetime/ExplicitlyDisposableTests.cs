using Microsoft.Practices.Unity;
using NUnit.Framework;
using Portoa.Web.Unity.Lifetime;

namespace Portoa.Web.Tests.Unity.Lifetime {
	[TestFixture]
	public class ExplicitlyDisposableTests {
		[Test]
		public void Should_explicitly_dispose_object_that_does_not_implement_IDisposable() {
			var foo = new Foo { Count = 10 };

			Assert.That(foo.Count, Is.EqualTo(10));
			using (var container = new UnityContainer()) {
				container.RegisterInstance(foo, new ExplicitlyDisposableLifetimeManager<Foo>(new MyLifetimeManager(), f => f.Close()));
			}
			Assert.That(foo.Count, Is.EqualTo(0));
		}

		private class Foo {
			public int Count { get; set; }

			public void Close() {
				Count = 0;
			}
		}

		private class MyLifetimeManager : LifetimeManager {
			private object value;
			public override object GetValue() {
				return value;
			}

			public override void SetValue(object newValue) {
				value = newValue;	
			}

			public override void RemoveValue() {
				value = null;
			}
		}
	}
}