using System;
using Microsoft.Practices.Unity;
using Portoa.Logging;

namespace Portoa.Web.Unity {
	/// <summary>
	/// Service provider that uses an <c>IUnityContainer</c> to instantiate and locate services
	/// </summary>
	[DoNotLog]
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