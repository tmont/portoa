using System;
using JetBrains.Annotations;

namespace Portoa.Web.ErrorHandling {

	/// <summary>
	/// Generic model for displaying errors where the currently logged in user is needed
	/// </summary>
	/// <typeparam name="T">The user type</typeparam>
	public class ErrorModel<T> : ErrorModel {

		/// <summary>
		/// The currently logged in user, or <c>null</c> if no one is logged in
		/// </summary>
		[CanBeNull]
		public T User { get; set; }
	}

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