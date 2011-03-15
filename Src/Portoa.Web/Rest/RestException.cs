using System;

namespace Portoa.Web.Rest {
	public class RestException : Exception {
		public RestException(string message = null, Exception innerException = null) : base(message, innerException) { }
	}
}