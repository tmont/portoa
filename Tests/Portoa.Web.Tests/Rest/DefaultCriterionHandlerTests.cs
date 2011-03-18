using System;
using NUnit.Framework;
using Portoa.Web.Rest;

namespace Portoa.Web.Tests.Rest {
	[TestFixture]
	public class DefaultCriterionHandlerTests {

		[Test]
		public void Should_handle_single_matching_value() {
			var criterion = new Criterion {
				FieldName = "foo",
				Values = new[] {
					new CriterionFieldValue { RawValue = "bar" }
				}
			};

			var expression = new DefaultCriterionHandler().HandleCriterion<MyEntity>(criterion);
			Assert.That(expression.Compile()(new MyEntity { Foo = "bar" }), Is.True);
		}

		[Test]
		public void Should_compose_expressions_using_AND() {
			var criterion = new Criterion {
				FieldName = "foo",
				Values = new[] {
					new CriterionFieldValue { RawValue = "bar" },
					new CriterionFieldValue { RawValue = "bar" }
				}
			};

			var expression = new DefaultCriterionHandler().HandleCriterion<MyEntity>(criterion);
			Assert.That(expression.Compile()(new MyEntity { Foo = "bar" }), Is.True);

			criterion = new Criterion {
				FieldName = "foo",
				Values = new[] {
					new CriterionFieldValue { RawValue = "bar" },
					new CriterionFieldValue { RawValue = "foo" }
				}
			};

			expression = new DefaultCriterionHandler().HandleCriterion<MyEntity>(criterion);
			Assert.That(expression.Compile()(new MyEntity { Foo = "bar" }), Is.False);
		}

		[Test]
		public void Should_compose_expressions_using_OR() {
			var criterion = new Criterion {
				FieldName = "foo",
				Values = new[] {
					new CriterionFieldValue { RawValue = "bar" },
					new CriterionFieldValue { RawValue = "foo", Modifier = FieldValueModifier.BooleanOr }
				}
			};

			var expression = new DefaultCriterionHandler().HandleCriterion<MyEntity>(criterion);
			Assert.That(expression.Compile()(new MyEntity { Foo = "bar" }), Is.True);

			expression = new DefaultCriterionHandler().HandleCriterion<MyEntity>(criterion);
			Assert.That(expression.Compile()(new MyEntity { Foo = "foo" }), Is.True);
		}

		[Test]
		public void Should_evaluate_greater_than() {
			var criterion = new Criterion {
				FieldName = "bar",
				Values = new[] {
					new CriterionFieldValue { RawValue = "5", Operator = FieldValueOperator.GreaterThan }
				}
			};

			var expression = new DefaultCriterionHandler().HandleCriterion<MyEntity>(criterion);
			Assert.That(expression.Compile()(new MyEntity { Bar = 4 }), Is.False);

			expression = new DefaultCriterionHandler().HandleCriterion<MyEntity>(criterion);
			Assert.That(expression.Compile()(new MyEntity { Bar = 6 }), Is.True);
		}

		[Test]
		public void Should_evaluate_greater_than_or_equal() {
			var criterion = new Criterion {
				FieldName = "bar",
				Values = new[] {
					new CriterionFieldValue { RawValue = "5", Operator = FieldValueOperator.GreaterThanOrEqualTo }
				}
			};

			var expression = new DefaultCriterionHandler().HandleCriterion<MyEntity>(criterion);
			Assert.That(expression.Compile()(new MyEntity { Bar = 4 }), Is.False);

			expression = new DefaultCriterionHandler().HandleCriterion<MyEntity>(criterion);
			Assert.That(expression.Compile()(new MyEntity { Bar = 5 }), Is.True);
		}

		[Test]
		public void Should_evaluate_less_than_or_equal() {
			var criterion = new Criterion {
				FieldName = "bar",
				Values = new[] {
					new CriterionFieldValue { RawValue = "5", Operator = FieldValueOperator.LessThanOrEqualTo }
				}
			};

			var expression = new DefaultCriterionHandler().HandleCriterion<MyEntity>(criterion);
			Assert.That(expression.Compile()(new MyEntity { Bar = 6 }), Is.False);

			expression = new DefaultCriterionHandler().HandleCriterion<MyEntity>(criterion);
			Assert.That(expression.Compile()(new MyEntity { Bar = 5 }), Is.True);
		}

		[Test]
		public void Should_evaluate_less_than() {
			var criterion = new Criterion {
				FieldName = "bar",
				Values = new[] {
					new CriterionFieldValue { RawValue = "5", Operator = FieldValueOperator.LessThan }
				}
			};

			var expression = new DefaultCriterionHandler().HandleCriterion<MyEntity>(criterion);
			Assert.That(expression.Compile()(new MyEntity { Bar = 5 }), Is.False);

			expression = new DefaultCriterionHandler().HandleCriterion<MyEntity>(criterion);
			Assert.That(expression.Compile()(new MyEntity { Bar = 4 }), Is.True);
		}

		[Test]
		public void Should_evaluate_not_equal() {
			var criterion = new Criterion {
				FieldName = "foo",
				Values = new[] {
					new CriterionFieldValue { RawValue = "bar", Operator = FieldValueOperator.NotEqual }
				}
			};

			var expression = new DefaultCriterionHandler().HandleCriterion<MyEntity>(criterion);
			Assert.That(expression.Compile()(new MyEntity { Foo = "bar" }), Is.False);

			expression = new DefaultCriterionHandler().HandleCriterion<MyEntity>(criterion);
			Assert.That(expression.Compile()(new MyEntity { Foo = "lulz" }), Is.True);
		}

		[Test]
		public void Should_evaluate_like() {
			var criterion = new Criterion {
				FieldName = "foo",
				Values = new[] {
					new CriterionFieldValue { RawValue = "ba", Operator = FieldValueOperator.Like }
				}
			};

			var expression = new DefaultCriterionHandler().HandleCriterion<MyEntity>(criterion);
			Assert.That(expression.Compile()(new MyEntity { Foo = "boring" }), Is.False);

			expression = new DefaultCriterionHandler().HandleCriterion<MyEntity>(criterion);
			Assert.That(expression.Compile()(new MyEntity { Foo = "bartender" }), Is.True);
		}

		[Test]
		public void Should_evaluate_complex_expression() {
			var criterion = new Criterion {
				FieldName = "bar",
				Values = new[] {
					new CriterionFieldValue { RawValue = "6", Operator = FieldValueOperator.NotEqual },
					new CriterionFieldValue { RawValue = "5", Operator = FieldValueOperator.Equal },
					new CriterionFieldValue { RawValue = "4", Operator = FieldValueOperator.GreaterThan },
					new CriterionFieldValue { RawValue = "0", Operator = FieldValueOperator.LessThan, Modifier = FieldValueModifier.BooleanOr}
				}
			};

			var expression = new DefaultCriterionHandler().HandleCriterion<MyEntity>(criterion);
			Assert.That(expression.Compile()(new MyEntity { Bar = 6 }), Is.False);

			expression = new DefaultCriterionHandler().HandleCriterion<MyEntity>(criterion);
			Assert.That(expression.Compile()(new MyEntity { Bar = 5 }), Is.True);

			expression = new DefaultCriterionHandler().HandleCriterion<MyEntity>(criterion);
			Assert.That(expression.Compile()(new MyEntity { Bar = -1 }), Is.True);
		}

		[Test, ExpectedException(typeof(UnknownCriterionException))]
		public void Should_handle_non_existent_property() {
			var criterion = new Criterion {
				FieldName = "asdf",
				Values = new[] {
					new CriterionFieldValue { RawValue = "ba", Operator = FieldValueOperator.Like }
				}
			};

			new DefaultCriterionHandler().HandleCriterion<MyEntity>(criterion);
		}

		[Test, ExpectedException(typeof(InvalidOperationException))]
		public void Should_handle_criterion_with_no_values() {
			var criterion = new Criterion {
				FieldName = "asdf"
			};

			new DefaultCriterionHandler().HandleCriterion<MyEntity>(criterion);
		}

		public class MyEntity {
			public string Foo { get; set; }
			public int Bar { get; set; }
		}
	}
}