using System;

namespace Portoa.EventLog {
	public class ServiceProviderEventLogPropertyHandlerFactory : IEventLogPropertyHandlerFactory {
		private readonly IServiceProvider serviceProvider;

		public ServiceProviderEventLogPropertyHandlerFactory(IServiceProvider serviceProvider) {
			this.serviceProvider = serviceProvider;
		}

		public IEventLogPropertyHandler Create(Type type) {
			return (IEventLogPropertyHandler)serviceProvider.GetService(type ?? typeof(DefaultEventLogPropertyHandler));
		}
	}
}