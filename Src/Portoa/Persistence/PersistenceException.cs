using System;

namespace Portoa.Persistence {
	/// <summary>
	/// Raised when an error occurred in the persistence layer
	/// </summary>
	public class PersistenceException : Exception {
		public PersistenceException(string message = null, Exception innerException = null) : base(message, innerException) { }
	}
}