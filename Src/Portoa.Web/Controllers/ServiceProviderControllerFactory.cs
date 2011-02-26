using System;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;

namespace Portoa.Web.Controllers {
	/// <summary>
	/// Controller factory that uses a service provider to resolve controllers
	/// </summary>
	[DebuggerNonUserCode]
	public class ServiceProviderControllerFactory : DefaultControllerFactory {
		private readonly IServiceProvider serviceProvider;

		public ServiceProviderControllerFactory(IServiceProvider serviceProvider) {
			this.serviceProvider = serviceProvider;
		}

		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType) {
			if (controllerType == null) {
				return base.GetControllerInstance(requestContext, controllerType);
			}

			return serviceProvider.GetService(controllerType) as IController;
		}
	}
}