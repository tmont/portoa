using System.Linq;
using NUnit.Framework;
using Portoa.Web.Rest;

namespace Portoa.Web.Tests.Rest.Parser {
	[TestFixture]
	public class ParserTests {

		private static void CompareFieldValue(CriterionFieldValue actual, CriterionFieldValue expected) {
			Assert.That(actual.Value, Is.EqualTo(expected.Value), string.Format("Field values don't match: {0} - {1}", actual, expected));
			Assert.That(actual.Modifier, Is.EqualTo(expected.Modifier), string.Format("Field value modifiers don't match: {0} - {1}", actual, expected));
			Assert.That(actual.Operator, Is.EqualTo(expected.Operator), string.Format("Field value operators don't match: {0} - {1}", actual, expected));
		}

		private static void CompareCriterion(Criterion actual, Criterion expected) {
			Assert.That(actual, Is.Not.Null, "Criterion does not exist");

			Assert.That(actual.FieldName, Is.EqualTo(expected.FieldName), "Field names don't match");
			Assert.That(actual.Values.Count(), Is.EqualTo(expected.Values.Count()), "Value counts don't match");

			for (var i = 0; i < actual.Values.Count(); i++) {
				CompareFieldValue(actual.Values.ElementAt(i), expected.Values.ElementAt(i));
			}
		}

		[Test]
		public void Should_create_criteria_set_during_parse() {
			var criteria = ParserHelper.Parse("foo/bar");
			CompareCriterion(criteria["foo"], new Criterion { FieldName = "foo", Values = new[] { new CriterionFieldValue { RawValue = "bar" } } });
		}

		[Test]
		public void Should_parse_multiple_values() {
			var criteria = ParserHelper.Parse("foo/bar,baz");

			CompareCriterion(criteria["foo"], new Criterion { 
				FieldName = "foo",
				Values = new[] { new CriterionFieldValue { RawValue = "bar" }, new CriterionFieldValue { RawValue = "baz" } } 
			});
		}

		[Test]
		public void Should_parse_multiple_criterion() {
			var criteria = ParserHelper.Parse("foo/bar/baz/bat");

			CompareCriterion(criteria["foo"], new Criterion { FieldName = "foo", Values = new[] { new CriterionFieldValue { RawValue = "bar" } } });
			CompareCriterion(criteria["baz"], new Criterion { FieldName = "baz", Values = new[] { new CriterionFieldValue { RawValue = "bat" } } });
		}

		[Test]
		public void Should_parse_boolean_or() {
			var criteria = ParserHelper.Parse("foo/bar|baz");

			CompareCriterion(criteria["foo"], new Criterion { 
				FieldName = "foo", 
				Values = new[] { 
					new CriterionFieldValue { RawValue = "bar" },
					new CriterionFieldValue { RawValue = "baz", Modifier = FieldValueModifier.BooleanOr } 
				} 
			});
		}

		[Test]
		public void Should_parse_like_operator() {
			var criteria = ParserHelper.Parse("foo/~bar");

			CompareCriterion(criteria["foo"], new Criterion {
				FieldName = "foo",
				Values = new[] { 
					new CriterionFieldValue { RawValue = "bar", Operator = FieldValueOperator.Like }
				}
			});
		}

		[Test]
		public void Should_parse_greater_than_operator() {
			var criteria = ParserHelper.Parse("foo/>bar");

			CompareCriterion(criteria["foo"], new Criterion {
				FieldName = "foo",
				Values = new[] { 
					new CriterionFieldValue { RawValue = "bar", Operator = FieldValueOperator.GreaterThan }
				}
			});
		}

		[Test]
		public void Should_parse_less_than_operator() {
			var criteria = ParserHelper.Parse("foo/<bar");

			CompareCriterion(criteria["foo"], new Criterion {
				FieldName = "foo",
				Values = new[] { 
					new CriterionFieldValue { RawValue = "bar", Operator = FieldValueOperator.LessThan }
				}
			});
		}

		[Test]
		public void Should_parse_less_than_or_equal_to_operator() {
			var criteria = ParserHelper.Parse("foo/<=bar");

			CompareCriterion(criteria["foo"], new Criterion {
				FieldName = "foo",
				Values = new[] { 
					new CriterionFieldValue { RawValue = "bar", Operator = FieldValueOperator.LessThanOrEqualTo }
				}
			});
		}

		[Test]
		public void Should_parse_greater_than_or_equal_to_operator() {
			var criteria = ParserHelper.Parse("foo/>=bar");

			CompareCriterion(criteria["foo"], new Criterion {
				FieldName = "foo",
				Values = new[] { 
					new CriterionFieldValue { RawValue = "bar", Operator = FieldValueOperator.GreaterThanOrEqualTo }
				}
			});
		}

		[Test]
		public void Should_parse_not_equal_operator() {
			var criteria = ParserHelper.Parse("foo/!bar");

			CompareCriterion(criteria["foo"], new Criterion {
				FieldName = "foo",
				Values = new[] { 
					new CriterionFieldValue { RawValue = "bar", Operator = FieldValueOperator.NotEqual }
				}
			});
		}
	}
}