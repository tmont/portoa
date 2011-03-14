using System;
using Portoa.Util;

namespace Portoa.Web.Unity {
	/// <summary>
	/// Raised when a container extension is expected to be registered with the container
	/// but it was not
	/// </summary>
	public class ExtensionDependencyException : Exception {
		/// <param name="unregisteredType">The container extension type</param>
		public ExtensionDependencyException(Type unregisteredType) 
			: base(string.Format("The container extension {0} was expected to be registered with the container", unregisteredType.GetFriendlyName())) { }
	}
}