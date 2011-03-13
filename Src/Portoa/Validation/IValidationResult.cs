using System.Collections.Generic;
using JetBrains.Annotations;

namespace Portoa.Validation {
	/// <summary>
	/// Represents the result of validation
	/// </summary>
	public interface IValidationResult {
		/// <summary>
		/// Gets or sets the error message
		/// </summary>
		[CanBeNull]
		string ErrorMessage { get; set; }

		/// <summary>
		/// Gets the member names of each property/field that did not validate
		/// </summary>
		IEnumerable<string> MemberNames { get; }
	}
}