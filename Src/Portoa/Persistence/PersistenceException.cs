using System;

namespace Portoa.Persistence {
	public class PersistenceException : Exception {
		public PersistenceException(string message = null, Exception innerException = null) : base(message, innerException) {}
	}
}