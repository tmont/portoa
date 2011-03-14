using NUnit.Framework;
using Portoa.Globalization;

namespace Portoa.Tests.Globalization {
	[TestFixture]
	public class CountryTests {
		[Test]
		public void Should_get_english_name_from_enum_value() {
			Assert.That(Country.UnitedStates.GetDisplayName(), Is.EqualTo("United States"));
		}
	}
}