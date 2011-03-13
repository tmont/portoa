using System.ComponentModel.DataAnnotations;

namespace Portoa.Validation.DataAnnotations {
	public static class ValidationResultExtensions {
		/// <summary>
		/// Wraps an <see cref="IValidationResult"/> in a <see cref="ValidationResult"/>
		/// </summary>
		public static ValidationResult ToResult(this IValidationResult result) {
			return result != null ? new ValidationResult(result.ErrorMessage, result.MemberNames) : null;
		}
	}
}