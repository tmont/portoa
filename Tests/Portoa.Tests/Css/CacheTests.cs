using System;
using System.Threading;
using NUnit.Framework;
using Portoa.Css;

namespace Portoa.Tests.Css {
	[TestFixture]
	public class CacheTests {
		[Test]
		public void Should_return_null_if_key_does_not_exist() {
			Assert.That(new InMemoryCacheStrategy().Get("foo"), Is.Null);
		}

		[Test]
		public void Should_noop_on_delete_if_key_does_not_exist() {
			new InMemoryCacheStrategy().Delete("foo");
		}

		[Test]
		public void Should_set_item_with_no_ttl() {
			var now = DateTime.UtcNow;
			var cache = new InMemoryCacheStrategy();
			cache.Set("foo", "bar");
			var item = cache.Get("foo");
			Assert.That(item.Created, Is.LessThanOrEqualTo(now));
			Assert.That(item.Created, Is.GreaterThanOrEqualTo(now));
			Assert.That(item.Css, Is.EqualTo("bar"));
			Assert.That(item.Ttl, Is.EqualTo(TimeSpan.MaxValue));
		}

		[Test]
		public void Should_delete_item_from_cache_if_ttl_expires() {
			var cache = new InMemoryCacheStrategy();
			var ttl = new TimeSpan(0, 0, 1);
			cache.Set("foo", "bar", new CssCacheOptions { Ttl = ttl });

			var item = cache.Get("foo");
			Assert.That(item.Css, Is.EqualTo("bar"));
			Assert.That(item.Ttl, Is.EqualTo(ttl));

			//wait a second to let the TTL expire
			Thread.Sleep(ttl.Add(new TimeSpan(0, 0, 1)));

			Assert.That(cache.Get("foo"), Is.Null);
		}
	}
}