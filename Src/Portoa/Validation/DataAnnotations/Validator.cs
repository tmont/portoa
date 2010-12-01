using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Portoa.Util;

namespace Portoa.Validation.DataAnnotations {
	public static class Validator {

		private static readonly ReflectionCache Cache;

		static Validator() {
			Cache = ReflectionCache.Instance;
		}

		/// <summary>
		/// Extension to System.ComponentModel.DataAnnotations.Validator.TryValidateObject(). This method validates
		/// the object, its properties and its fields.
		/// </summary>
		public static bool TryValidateObject(object instance, ValidationContext context, ICollection<ValidationResult> results, bool validateAllProperties = false) {
			System.ComponentModel.DataAnnotations.Validator.TryValidateObject(instance, context, results, validateAllProperties);

			return context
				.ObjectType
				.GetAllValidatableFields()
				.Aggregate(true, (current, field) => 
					TryValidateField(field, new ValidationContext(context.ObjectInstance, context.ServiceContainer, context.Items) { MemberName = field.Name }, results)
					&& current
				);
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