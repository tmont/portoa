using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Portoa.Util;
using DataAnnotationsValidator = System.ComponentModel.DataAnnotations.Validator;

namespace Portoa.Validation.DataAnnotations {
	/// <summary>
	/// Provides convenience methods for validating objects annotated with <see cref="ValidationAttribute"/>
	/// </summary>
	public static class Validator {
		/// <summary>
		/// Extension to <see cref="System.ComponentModel.DataAnnotations.Validator.TryValidateObject(object, ValidationContext, ICollection{ValidationResult}, bool)"/>. 
		/// This method validates the object, its properties and its fields (DataAnnotations does not validate the fields).
		/// </summary>
		public static bool TryValidateObject(object instance, ValidationContext context, ICollection<ValidationResult> results, bool validateAllProperties = false) {
			var isValid = DataAnnotationsValidator.TryValidateObject(instance, context, results, validateAllProperties);

			if (isValid || validateAllProperties) {
				foreach (var field in context.ObjectType.GetAllValidatableFields()) {
					isValid &= TryValidateField(field, new ValidationContext(context.ObjectInstance, context.ServiceContainer, context.Items) { MemberName = field.Name }, results);
					if (!isValid && !validateAllProperties) {
						break;
					}
				}
			}

			return isValid;
		}

		private static bool TryValidateField(FieldInfo fieldInfo, ValidationContext context, ICollection<ValidationResult> results) {
			var value = fieldInfo.GetValue(context.ObjectInstance);
			var originalCount = results.Count;

			results.AddRange(
				fieldInfo
					.GetAttributes<ValidationAttribute>()
					.Select(attribute => attribute.GetValidationResult(value, context))
					.Where(result => result != ValidationResult.Success)
			);

			return results.Count > originalCount;
		}
	}
}