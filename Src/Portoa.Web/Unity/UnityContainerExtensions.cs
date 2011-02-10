using System;
using System.Linq;
using Microsoft.Practices.Unity;

namespace Portoa.Web.Unity {
	/// <summary>
	/// Extension methods for <c>UnityContainer</c>
	/// </summary>
	public static class UnityContainerExtensions {
		/// <summary>
		/// Adds an extension if it hasn't already been registered with the container
		/// </summary>
		public static IUnityContainer AddExtensionOnce<T>(this IUnityContainer container) where T : UnityContainerExtension, new() {
			if (container.Configure<T>() == null) {
				container.AddNewExtension<T>();
			}

			return container;
		}

		/// <summary>
		/// Determines whether any of the given types are registered in the container
		/// </summary>
		public static bool AnyAreRegistered(this IUnityContainer container, params Type[] types) {
			return types.Any(type => container.IsRegistered(type));
		}

		/// <summary>
		/// Determines if all of the given types are registered in the container
		/// </summary>
		/// <param name="container"></param>
		/// <param name="types"></param>
		/// <returns></returns>
		public static bool AllAreRegistered(this IUnityContainer container, params Type[] types) {
			return types.All(type => container.IsRegistered(type));
		}
	}
}