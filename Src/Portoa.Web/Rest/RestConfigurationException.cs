using System;

namespace Portoa.Web.Rest {
	/// <summary>
	/// Raised when the REST engine was not properly configured
	/// </summary>
	public class RestConfigurationException : RestException {
		public RestConfigurationException(string message, Exception innerException = null) : base(message, innerException) {}
	}
}