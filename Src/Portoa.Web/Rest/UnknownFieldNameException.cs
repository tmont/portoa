namespace Portoa.Web.Rest {
	/// <summary>
	/// Raised when an unknown field is encountered
	/// </summary>
	public class UnknownFieldNameException : RestException {
		/// <param name="fieldName">The name of the unknown field</param>
		public UnknownFieldNameException(string fieldName) : base(string.Format("Unknown field \"{0}\"", fieldName)) { }
	}
}