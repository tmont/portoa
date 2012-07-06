using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Portoa.Util;

namespace Portoa.Validation.DataAnnotations {
	/// <summary>
	/// Default entity validator for validating entities annotated with attributes
	/// derived from <see cref="ValidationAttribute"/>
	/// </summary>
	public class EntityValidator : IEntityValidator {
		private readonly IServiceProvider provider;
		private readonly IValidationAttributeProvider attributeProvider;

		public EntityValidator(IServiceProvider provider, IValidationAttributeProvider attributeProvider) {
			this.provider = provider;
			this.attributeProvider = attributeProvider ?? new ReflectionValidationAttributeProvider();
		}

		public IEnumerable<IValidationResult> Validate(object instance, bool stopOnFirstError = false) {
			var results = new Collection<ValidationResult>();
			Validate(new ValidationContext(instance, provider, null), results, validateAllProperties: !stopOnFirstError);
			return results.Select(result => new ResultAdapter(result));
		}

		private void Validate(ValidationContext context, ICollection<ValidationResult> results, bool validateAllProperties = false) {
			var isValid = true;

			foreach (var property in context.ObjectType.GetAllValidatableProperties()) {
				isValid &= TryValidate(property, new ValidationContext(context.ObjectInstance, context.ServiceContainer, context.Items) { MemberName = property.Name }, results);
				if (!isValid && !validateAllProperties) {
					break;
				}
			}

			if (!isValid && !validateAllProperties) {
				return;
			}

			foreach (var field in context.ObjectType.GetAllValidatableFields()) {
				isValid &= TryValidate(field, new ValidationContext(context.ObjectInstance, context.ServiceContainer, context.Items) { MemberName = field.Name }, results);
				if (!isValid && !validateAllProperties) {
					break;
				}
			}
		}

		private bool TryValidate(PropertyInfo property, ValidationContext context, ICollection<ValidationResult> results) {
			return ValidateMember(property, context, results, property.GetValue(context.ObjectInstance, new object[0]));
		}

		private bool TryValidate(FieldInfo field, ValidationContext context, ICollection<ValidationResult> results) {
			return ValidateMember(field, context, results, field.GetValue(context.ObjectInstance));
		}

		private bool ValidateMember(ICustomAttributeProvider member, ValidationContext context, ICollection<ValidationResult> results, object value) {
			var originalCount = results.Count;
			results.AddRange(
				attributeProvider
					.GetAttributes(member)
					.Select(attribute => attribute.GetValidationResult(value, context))
					.Where(result => result != ValidationResult.Success)
			);

			return results.Count > originalCount;
		}
	}
}