using System;
using System.Net;

namespace Portoa.Web.Rest {
	/// <summary>
	/// General exception raised when an error occurs while processing a RESTful request
	/// </summary>
	public class RestException : Exception {
		public RestException(string message = null, Exception innerException = null) : base(message, innerException) {
			RecommendedStatusCode = HttpStatusCode.BadRequest;
		}

		/// <summary>
		/// Gets or sets the recommended <see cref="HttpStatusCode"/> that should be sent
		/// back in the response
		/// </summary>
		public virtual HttpStatusCode RecommendedStatusCode { get; set; }
	}
}