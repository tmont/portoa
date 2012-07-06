using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Portoa.Util;

namespace Portoa.Validation.DataAnnotations {
	/// <summary>
	/// Provides a way of retrieving the attributes that determine how objects
	/// get validated
	/// </summary>
	public interface IValidationAttributeProvider {
		IEnumerable<ValidationAttribute> GetAttributes(ICustomAttributeProvider attributeProvider);
	}

	/// <summary>
	/// Retrieves validation attributes via simple reflection
	/// </summary>
	public class ReflectionValidationAttributeProvider : IValidationAttributeProvider {
		public IEnumerable<ValidationAttribute> GetAttributes(ICustomAttributeProvider attributeProvider) {
			return attributeProvider.GetAttributes<ValidationAttribute>();
		}
	}
}