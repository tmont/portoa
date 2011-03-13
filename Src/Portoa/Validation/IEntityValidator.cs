using System.Collections.Generic;

namespace Portoa.Validation {
	/// <summary>
	/// Provides an interface to perform validation on an entity
	/// </summary>
	public interface IEntityValidator {
		/// <summary>
		/// Validates the given entity
		/// </summary>
		/// <param name="instance">The entity to validate</param>
		/// <param name="stopOnFirstError">Whether to stop validating after the first error is encountered; default is <c>false</c></param>
		IEnumerable<IValidationResult> Validate(object instance, bool stopOnFirstError = false);
	}
}