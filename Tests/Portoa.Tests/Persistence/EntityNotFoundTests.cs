using JetBrains.Annotations;
using NUnit.Framework;
using Portoa.Persistence;

namespace Portoa.Tests.Persistence {
	[TestFixture]
	public class EntityNotFoundTests {
		[Test]
		public void Should_properly_format_exception_message() {
			var exception = new EntityNotFoundException<MyEntity>(9);
			Assert.That(exception.Message, Is.EqualTo("Entity of type MyEntity not found with ID 9"));
		}

		[UsedImplicitly]
		private class MyEntity : Entity<int> { }
	}
}