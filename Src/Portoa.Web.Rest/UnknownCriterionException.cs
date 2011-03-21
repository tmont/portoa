using System;

namespace Portoa.Web.Rest {
	/// <summary>
	/// Raised when an unknown criterion is encountered
	/// </summary>
	public class UnknownCriterionException : RestException {
		public UnknownCriterionException(string fieldName, Exception innerException = null) : base(string.Format("Unknown field name \"{0}\"", fieldName), innerException) { }
	}
}