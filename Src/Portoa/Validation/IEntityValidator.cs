using System.Collections.Generic;

namespace Portoa.Validation {
	public interface IEntityValidator {
		IEnumerable<IValidationResult> Validate(object instance);
	}
}