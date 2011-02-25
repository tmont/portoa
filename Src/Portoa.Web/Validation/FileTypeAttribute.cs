using System.ComponentModel.DataAnnotations;
using System.Web;
using JetBrains.Annotations;

namespace Portoa.Web.Validation {
	/// <summary>
	/// Verifies that an uploaded file has a certain mimetype
	/// </summary>
	public class FileTypeAttribute : ValidationAttribute {
		public FileTypeAttribute([NotNull]string mimeType) {
			MimeType = mimeType;
		}

		/// <summary>
		/// Gets or sets the expected mimetype (i.e. text/plain, image/png, etc.)
		/// </summary>
		public string MimeType { get; set; }

		public override bool IsValid(object value) {
			var file = value as HttpPostedFileBase;

			if (file == null) {
				return false;
			}

			return file.ContentType == MimeType;
		}
	}
}