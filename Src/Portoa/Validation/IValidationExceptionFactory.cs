using System;
using System.Collections.Generic;

namespace Portoa.Validation {
	/// <summary>
	/// Provides a mechanism for creating an exception out of a collection of
	/// <see cref="IValidationResult"/>
	/// </summary>
	public interface IValidationExceptionFactory {
		/// <summary>
		/// Creates an exception from a collection of <see cref="IValidationResult"/>
		/// </summary>
		Exception Create(IEnumerable<IValidationResult> results);
	}
}