using JetBrains.Annotations;

namespace Portoa.Web.Models {
	/// <summary>
	/// Represents a simple, consistent object to use for AJAX responses
	/// </summary>
	public class JsonResponse {
		public JsonResponse() {
			Data = new object();
		}

		/// <summary>
		/// Gets or sets the error message that occurred. If <c>null</c>, it's assumed
		/// that no error occurred.
		/// </summary>
		[CanBeNull]
		public string Error { get; set; }

		/// <summary>
		/// Gets or sets any extra data the client may be expecting. This value should
		/// not be null.
		/// </summary>
		[NotNull]
		public object Data { get; set; }
	}
}