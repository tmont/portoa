using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Portoa.Validation.DataAnnotations {
	public class AccumulatedValidationException : ValidationException, IValidationResultsProvider {
		private readonly List<IValidationResult> results = new List<IValidationResult>();

		public AccumulatedValidationException(IEnumerable<IValidationResult> validationResults) : this(validationResults.First().ToResult(), null, null) {
			results.AddRange(validationResults);
		}

		public AccumulatedValidationException(string message = null, Exception innerException = null) : base(message, innerException) { }
		public AccumulatedValidationException(string errorMessage, ValidationAttribute validatingAttribute, object value) : base(errorMessage, validatingAttribute, value) { }
		public AccumulatedValidationException(ValidationResult validationResult, ValidationAttribute validatingAttribute, object value) : base(validationResult, validatingAttribute, value) { }

		public IEnumerable<IValidationResult> Results { get { return results; } }
	}
}