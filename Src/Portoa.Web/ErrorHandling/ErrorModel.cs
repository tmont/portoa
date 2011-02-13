using System;
using JetBrains.Annotations;

namespace Portoa.Web.ErrorHandling {
	/// <summary>
	/// Generic model for displaying errors
	/// </summary>
	public class ErrorModel {
		/// <summary>
		/// Gets or sets the exception that occurred
		/// </summary>
		[CanBeNull]
		public Exception Exception { get; set; }
	}
}