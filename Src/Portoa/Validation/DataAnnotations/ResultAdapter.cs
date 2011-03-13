using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Portoa.Validation.DataAnnotations {
	/// <summary>
	/// Provides a bridge between <see cref="ValidationResult"/> and <see cref="IValidationResult"/>
	/// </summary>
	public class ResultAdapter : IValidationResult {
		private readonly ValidationResult validationResult;

		public ResultAdapter(ValidationResult validationResult) {
			this.validationResult = validationResult;
		}

		public string ErrorMessage { get { return validationResult.ErrorMessage; } set { validationResult.ErrorMessage = value; } }
		public IEnumerable<string> MemberNames { get { return validationResult.MemberNames; } }
	}
}