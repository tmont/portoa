using System;

namespace Portoa.Web.Unity {
	/// <summary>
	/// Signifies that this container extension depends on other container extensions
	/// already being registered with the container. To make use of this attribute
	/// make sure you inherit from <see cref="VerifiableContainerExtension"/>.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
	public sealed class DependsOnExtensionsAttribute : Attribute {
		/// <param name="dependentTypes">The dependent extension interface types</param>
		public DependsOnExtensionsAttribute(params Type[] dependentTypes) {
			if (dependentTypes.Length == 0) {
				throw new ArgumentException("Must specify at least one dependent type");
			}

			DependentTypes = dependentTypes;
		}

		/// <summary>
		/// Gets the dependent extension interface types
		/// </summary>
		public Type[] DependentTypes { get; private set; }
	}
}