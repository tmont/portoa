using System;
using System.Collections.Generic;

namespace Portoa.Validation {
	public interface IValidationExceptionFactory {
		Exception Create(IEnumerable<IValidationResult> results);
	}
}