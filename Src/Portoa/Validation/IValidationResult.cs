using System.Collections.Generic;

namespace Portoa.Validation {
	public interface IValidationResult {
		string ErrorMessage { get; set; }
		IEnumerable<string> MemberNames { get; }
	}

}