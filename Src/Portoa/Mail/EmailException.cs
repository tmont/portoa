using System;

namespace Portoa.Mail {
	/// <summary>
	/// Exception that is raised when an error occurs while attempting to deliver mail
	/// </summary>
	public sealed class EmailException : Exception {
		public EmailException(string message = null, Exception innerException = null) : base(message, innerException) { }
	}
}