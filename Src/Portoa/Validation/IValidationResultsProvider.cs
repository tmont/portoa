using System.Collections.Generic;

namespace Portoa.Validation {
	public interface IValidationResultsProvider {
		IEnumerable<IValidationResult> Results { get; }
	}
}