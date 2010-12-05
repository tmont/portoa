using System;

namespace Portoa.EventLog {
	public class ServiceProviderEventLogPropertyHandlerFactory : IEventLogPropertyHandlerFactory {
		private static readonly Type DefaultHandlerType = typeof(DefaultEventLogPropertyHandler);
		private readonly IServiceProvider serviceProvider;
		
		public ServiceProviderEventLogPropertyHandlerFactory(IServiceProvider serviceProvider) {
			this.serviceProvider = serviceProvider;
		}

		public IEventLogPropertyHandler Create(Type type) {
			return (IEventLogPropertyHandler)serviceProvider.GetService(type ?? DefaultHandlerType);
		}
	}
}