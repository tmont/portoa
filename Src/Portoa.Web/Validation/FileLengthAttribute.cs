using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Portoa.Web.Validation {
	/// <summary>
	/// Verifies that an uploaded file's length lies within a range
	/// </summary>
	public class FileLengthAttribute : ValidationAttribute {
		/// <param name="maxLength">The (inclusive) maximum length of the uploaded file</param>
		public FileLengthAttribute(int maxLength) {
			MaxLength = maxLength;
		}

		/// <summary>
		/// Gets or sets the (inclusive) maximum length of the uploaded file
		/// </summary>
		public int MaxLength { get; set; }

		/// <summary>
		/// Gets or sets the (inclusive) minimum length of the uploaded file
		/// </summary>
		public int MinLength { get; set; }

		public override bool IsValid(object value) {
			var file = value as HttpPostedFileBase;

			if (file == null) {
				return false;
			}

			return file.ContentLength >= MinLength && file.ContentLength <= MaxLength;
		}
	}
}