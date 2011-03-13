using System;
using System.Collections.Generic;

namespace Portoa.Validation.DataAnnotations {
	public class ExceptionFactory : IValidationExceptionFactory {
		public Exception Create(IEnumerable<IValidationResult> results) {
			return new AggregateValidationException(results);
		}
	}
}