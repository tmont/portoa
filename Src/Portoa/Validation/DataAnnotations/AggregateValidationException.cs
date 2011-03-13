using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Portoa.Validation.DataAnnotations {
	/// <summary>
	/// Represents a collection of <see cref="ValidationException"/>
	/// </summary>
	public class AggregateValidationException : ValidationException, IValidationResultsProvider {
		private readonly List<IValidationResult> results = new List<IValidationResult>();

		public AggregateValidationException(IEnumerable<IValidationResult> validationResults) : this(validationResults.First().ToResult(), null, null) {
			results.AddRange(validationResults);
		}

		public AggregateValidationException(string message = null, Exception innerException = null) : base(message, innerException) { }
		public AggregateValidationException(string errorMessage, ValidationAttribute validatingAttribute, object value) : base(errorMessage, validatingAttribute, value) { }
		public AggregateValidationException(ValidationResult validationResult, ValidationAttribute validatingAttribute, object value) : base(validationResult, validatingAttribute, value) { }

		public IEnumerable<IValidationResult> Results { get { return results; } }
	}
}