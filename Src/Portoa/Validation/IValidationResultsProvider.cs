using System.Collections.Generic;

namespace Portoa.Validation {
	/// <summary>
	/// Provides an interface to retrieve a collection of <see cref="IValidationResult"/>
	/// </summary>
	public interface IValidationResultsProvider {
		/// <summary>
		/// Gets the validation results
		/// </summary>
		IEnumerable<IValidationResult> Results { get; }
	}
}