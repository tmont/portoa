using System.ComponentModel.DataAnnotations;

namespace Portoa.Validation.DataAnnotations {
	/// <summary>
	/// Validates that the value is greater than zero
	/// </summary>
	public sealed class GreaterThanZeroAttribute : ValidationAttribute {

		public GreaterThanZeroAttribute() : this("Value must be greater than zero.") { }
		public GreaterThanZeroAttribute(string errorMessage) : base(errorMessage) { }

		public override bool IsValid(object value) {
			if (value == null) {
				return false;
			}

			double d;
			if (!double.TryParse(value.ToString(), out d)) {
				return false;
			}

			return d > 0;
		}

	}
}