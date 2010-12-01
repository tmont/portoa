using System;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;

namespace Portoa.Web {
	/// <summary>
	/// Controller factory that uses a service provider to resolve controllers
	/// </summary>
	[DebuggerNonUserCode]
	public class ServiceProviderControllerFactory : DefaultControllerFactory {
		private readonly IServiceProvider serviceProvider;
		private readonly IActionInvoker actionInvoker;

		public ServiceProviderControllerFactory(IServiceProvider serviceProvider, IActionInvoker actionInvoker) {
			this.serviceProvider = serviceProvider;
			this.actionInvoker = actionInvoker;
		}

		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType) {
			if (controllerType == null) {
				return base.GetControllerInstance(requestContext, controllerType);
			}

			var controllerImpl = serviceProvider.GetService(controllerType) as IController;
			var controller = controllerImpl as Controller;
			if (controller != null) {
				controller.ActionInvoker = actionInvoker;
			}

			return controller ?? controllerImpl;
		}
	}
}