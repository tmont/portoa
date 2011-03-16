using Moq;
using NUnit.Framework;
using Portoa.Web.Rest;

namespace Portoa.Web.Tests.Rest {
	[TestFixture]
	public class IdParserTests {
		[Test]
		public void Should_parse_identity_id() {
			var parser = new IdentityIdParser();
			Assert.That(parser.ParseId("1"), Is.EqualTo("1"));
			Assert.That(parser.ParseId("9"), Is.EqualTo("9"));
			Assert.That(parser.ParseId("100"), Is.EqualTo("100"));
		}

		[Test, ExpectedException(typeof(InvalidIdException))]
		public void Should_not_allow_non_integers() {
			new IdentityIdParser().ParseId("foo");
		}

		[Test, ExpectedException(typeof(InvalidIdException))]
		public void Should_not_allow_integers_less_than_one() {
			new IdentityIdParser().ParseId("0");
		}

		[Test]
		public void Should_allow_fetching_all_records_by_default() {
			Assert.That(new Mock<RestIdParserBase> { CallBase = true }.Object.AllowFetchAll, Is.True);
		}

		[Test]
		public void Default_fetch_all_id_value_should_be_all() {
			Assert.That(new Mock<RestIdParserBase> { CallBase = true }.Object.FetchAllIdValue, Is.EqualTo("all"));
		}
	}
}