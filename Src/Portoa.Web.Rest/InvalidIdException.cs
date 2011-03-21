using System;

namespace Portoa.Web.Rest {
	/// <summary>
	/// Raised when an id in a RESTful request could not be parsed
	/// </summary>
	public class InvalidIdException : RestException {
		public InvalidIdException(string idValue, Exception innerException = null) : base(string.Format("Invalid ID value: \"{0}\"", idValue), innerException) { }
	}
}