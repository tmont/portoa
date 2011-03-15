using System;

namespace Portoa.Web.Rest {
	/// <summary>
	/// Raised when an unknown criterion is encountered
	/// </summary>
	public class UnknownCriterionException : RestException {
		public UnknownCriterionException(string message = null, Exception innerException = null) : base(message, innerException) { }
	}
}