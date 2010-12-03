using System;
using System.Linq;
using Microsoft.Practices.Unity;

namespace Portoa.Web.Unity {
	public static class UnityContainerExtensions {
		public static IUnityContainer AddExtensionOnce<T>(this IUnityContainer container) where T : UnityContainerExtension, new() {
			if (container.Configure<T>() == null) {
				container.AddNewExtension<T>();
			}
			return container;
		}

		public static bool AnyAreRegistered(this IUnityContainer container, params Type[] types) {
			return types.Any(type => container.IsRegistered(type));
		}

		public static bool AllAreRegistered(this IUnityContainer container, params Type[] types) {
			return types.All(type => container.IsRegistered(type));
		}
	}
}