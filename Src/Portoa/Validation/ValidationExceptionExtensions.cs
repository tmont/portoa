using System.Linq;

namespace Portoa.Validation {
	public static class ValidationExceptionExtensions {
		public static string CombineErrorMessages(this IValidationResultsProvider resultsProvider, string separator = "\n") {
			return resultsProvider.Results.Aggregate("Validation errors:", (current, next) => current + separator + next.ErrorMessage);
		}
	}
}