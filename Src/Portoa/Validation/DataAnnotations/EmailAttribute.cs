using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Portoa.Validation.DataAnnotations {
	/// <summary>
	/// Validates that a string is a valid email address. Set AllowEmpty to true
	/// if null and empty strings should not be validated.
	/// </summary>
	public sealed class EmailAttribute : ValidationAttribute {

		#region Hideous email regular expression
		private static readonly Regex EmailRegex = new Regex("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
		#endregion

		public EmailAttribute() : base("Email address is invalid.") { }

		/// <summary>
		/// Gets or sets whether to allow empty strings
		/// </summary>
		public bool AllowEmpty { get; set; }

		public override bool IsValid(object value) {
			if (value == null || (value is string && string.IsNullOrEmpty((string)value))) {
				return AllowEmpty;
			}

			return EmailRegex.IsMatch((string)value);
		}
	}
}