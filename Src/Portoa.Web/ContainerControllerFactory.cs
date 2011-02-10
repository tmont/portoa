using System;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;

namespace Portoa.Web {
	/// <summary>
	/// Uses a container to create controllers
	/// </summary>
	[DebuggerNonUserCode]
	public class ContainerControllerFactory : DefaultControllerFactory {
		private readonly IUnityContainer container;

		public ContainerControllerFactory(IUnityContainer container) {
			this.container = container;
		}

		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType) {
			if (controllerType == null) {
				return base.GetControllerInstance(requestContext, controllerType);
			}

			return container.Resolve(controllerType) as IController;
		}
	}
}