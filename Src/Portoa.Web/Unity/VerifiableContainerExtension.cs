using System.Linq;
using Microsoft.Practices.Unity;
using Portoa.Util;

namespace Portoa.Web.Unity {
	/// <summary>
	/// Upon initialization verifies that all extension dependencies are valid
	/// in accordance with <see cref="DependsOnExtensionsAttribute"/>
	/// </summary>
	public abstract class VerifiableContainerExtension : UnityContainerExtension {
		protected override sealed void Initialize() {
			var dependsOnAttribute = GetType().GetAttributes<DependsOnExtensionsAttribute>().FirstOrDefault();
			if (dependsOnAttribute != null) {
				var unregisteredType = dependsOnAttribute.DependentTypes.FirstOrDefault(type => Container.Configure(type) == null);
				if (unregisteredType != null) {
					throw new ExtensionDependencyException(unregisteredType);
				}
			}

			DoInitialize();
		}

		/// <summary>
		/// Initializes the container extension
		/// </summary>
		protected abstract void DoInitialize();
	}
}