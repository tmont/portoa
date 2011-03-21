using System;

namespace Portoa.Web.Rest {
	public sealed class CriterionHandlingException : RestException {
		public CriterionHandlingException(string errorMessage, Exception innerException = null) : base(errorMessage, innerException) { }
	}
}