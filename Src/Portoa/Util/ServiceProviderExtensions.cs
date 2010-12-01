using System;

namespace Portoa.Util {
	public static class ServiceProviderExtensions {
		public static T GetService<T>(this IServiceProvider provider) {
			return (T)provider.GetService(typeof(T));
		}
	}
}