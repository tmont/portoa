using System;

namespace Portoa.Search {
	/// <summary>
	/// Raised when an error occurs while reading/writing a search index
	/// </summary>
	public class SearchIndexException : Exception {
		public SearchIndexException(string message = null, Exception innerException = null) : base(message, innerException) { }
	}
}