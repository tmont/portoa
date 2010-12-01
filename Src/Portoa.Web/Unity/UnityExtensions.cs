using Microsoft.Practices.Unity;

namespace Portoa.Web.Unity {
	public static class UnityExtensions {
		public static IUnityContainer AddExtensionOnce<T>(this IUnityContainer container) where T : UnityContainerExtension, new() {
			if (container.Configure<T>() == null) {
				container.AddNewExtension<T>();
			}
			return container;
		}
	}
}