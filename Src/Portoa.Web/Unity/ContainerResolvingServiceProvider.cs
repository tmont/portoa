using System;
using Microsoft.Practices.Unity;

namespace Portoa.Web.Unity {
	public sealed class ContainerResolvingServiceProvider : IServiceProvider {
		private readonly IUnityContainer container;

		public ContainerResolvingServiceProvider(IUnityContainer container) {
			this.container = container;
		}

		public object GetService(Type serviceType) {
			return container.Resolve(serviceType);
		}
	}
}