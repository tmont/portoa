using NUnit.Framework;
using Portoa.Persistence;

namespace Portoa.Tests.Persistence {

	[TestFixture]
	public class EntityTests {
		[Test]
		public void Should_be_equal_if_ids_are_equal() {
			var entity = new MockEntity { Id = 1 };
			var other = new MockEntity { Id = 1 };

			Assert.That(entity, Is.EqualTo(other));
		}

		[Test]
		public void Should_not_be_equal_if_ids_are_not_equal() {
			var entity = new MockEntity { Id = 1 };
			var other = new MockEntity { Id = 2 };

			Assert.That(entity, Is.Not.EqualTo(other));
		}

		[Test]
		public void Should_not_be_equal_if_not_the_same_type() {
			var entity = new MockEntity { Id = 1 };
			var other = new DerivedMockEntity { Id = 1 };

			Assert.That(entity, Is.Not.EqualTo(other));
		}

		[Test]
		public void Should_not_be_equal_if_one_is_null() {
			var entity = new MockEntity { Id = 1 };
			MockEntity other = null;

			Assert.That(entity, Is.Not.EqualTo(other));
		}

		[Test]
		public void Should_be_equal_if_transient_and_reference_same_object() {
			var entity = new MockEntity();
			var other = entity;

			Assert.That(entity.IsTransient(), Is.True);
			Assert.That(other.IsTransient(), Is.True);
			Assert.That(entity, Is.EqualTo(other));
		}

		[Test]
		public void Should_be_transient_even_if_not_strongly_typed() {
			object entity = new MockEntity();
			Assert.That(entity.IsTransient(), Is.True);
		}

		public class MockEntity : Entity<int> { }
		public class DerivedMockEntity : MockEntity { }
	}

	
}
