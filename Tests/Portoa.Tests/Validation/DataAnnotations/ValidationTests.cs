using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NUnit.Framework;
using Portoa.Validation.DataAnnotations;

namespace Portoa.Tests.Validation.DataAnnotations {
	[TestFixture]
	public class ValidationTests {
		[Test]
		public void Should_validate_all_properties_and_fields() {
			var validator = new EntityValidator(new ActivatorServiceProvider());
			var entity = new Validatable();
			var results = validator.Validate(entity);

			Assert.That(results.Count(), Is.EqualTo(2));

			var result = results.First();
			Assert.That(result.ErrorMessage, Is.EqualTo("The NotNullProperty field is required."));
			Assert.That(result.MemberNames.Count(), Is.EqualTo(1));
			Assert.That(result.MemberNames.First(), Is.EqualTo("NotNullProperty"));

			result = results.Last();
			Assert.That(result.ErrorMessage, Is.EqualTo("The notNullField field is required."));
			Assert.That(result.MemberNames.Count(), Is.EqualTo(1));
			Assert.That(result.MemberNames.First(), Is.EqualTo("notNullField"));
		}

		[Test]
		public void Should_stop_validating_after_first_error() {
			var validator = new EntityValidator(new ActivatorServiceProvider());
			var entity = new Validatable();
			var results = validator.Validate(entity, stopOnFirstError: true);

			Assert.That(results.Count(), Is.EqualTo(1));

			var result = results.First();
			Assert.That(result.ErrorMessage, Is.EqualTo("The NotNullProperty field is required."));
			Assert.That(result.MemberNames.Count(), Is.EqualTo(1));
			Assert.That(result.MemberNames.First(), Is.EqualTo("NotNullProperty"));
		}

		[Test]
		public void Should_validate_email() {
			var email = new EmailAttribute();
			Assert.That(email.IsValid("foo@bar.com"), Is.True);
			Assert.That(email.IsValid("foo+foo@bar.com"), Is.True);
			Assert.That(email.IsValid("foo/foo@bar.com"), Is.True);
			Assert.That(email.IsValid("foo@bar.f"), Is.True);

			Assert.That(email.IsValid("foo@bar."), Is.False);
			Assert.That(email.IsValid("foo@bar"), Is.False);
			Assert.That(email.IsValid("foo"), Is.False);
			Assert.That(email.IsValid("foo..@bar.com"), Is.False);
		}

		[Test]
		public void Should_be_greater_than_zero() {
			var greaterThanZero = new GreaterThanZeroAttribute();
			Assert.That(greaterThanZero.IsValid("1"), Is.True);
			Assert.That(greaterThanZero.IsValid(1), Is.True);
			Assert.That(greaterThanZero.IsValid(1.0), Is.True);
			Assert.That(greaterThanZero.IsValid(1f), Is.True);
			Assert.That(greaterThanZero.IsValid(1m), Is.True);

			Assert.That(greaterThanZero.IsValid(0), Is.False);
			Assert.That(greaterThanZero.IsValid(0m), Is.False);
			Assert.That(greaterThanZero.IsValid(0.0), Is.False);
			Assert.That(greaterThanZero.IsValid(0f), Is.False);
			Assert.That(greaterThanZero.IsValid("0"), Is.False);
		}

		private class ActivatorServiceProvider : IServiceProvider {
			public object GetService(Type serviceType) {
				return Activator.CreateInstance(serviceType);
			}
		}

		private class Validatable {
			private object notValidated;

			[Required]
			private string notNullField;

			public Validatable(string notNullField = null) {
				this.notNullField = notNullField;
			}

			[Required]
			public string NotNullProperty { get; set; }
		}
	}
}