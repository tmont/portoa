using System;
using System.Collections.Generic;

namespace Portoa.Validation {
	public class ValidationFailedException : Exception, IValidationResultsProvider {
		private readonly IEnumerable<IValidationResult> results;

		public ValidationFailedException(IEnumerable<IValidationResult> results) : this(results.CombineErrorMessages()) {
			this.results = results;
		}

		public ValidationFailedException(string errorMessage = null, Exception innerException = null) : base(errorMessage, innerException) { }

		public IEnumerable<IValidationResult> Results { get { return results; } }
	}
}