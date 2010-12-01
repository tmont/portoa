using System.ComponentModel.DataAnnotations;

namespace Portoa.Validation.DataAnnotations {
	public static class ValidationResultExtensions {
		public static ValidationResult ToResult(this IValidationResult result) {
			return result != null ? new ValidationResult(result.ErrorMessage, result.MemberNames) : null;
		}
	}
}