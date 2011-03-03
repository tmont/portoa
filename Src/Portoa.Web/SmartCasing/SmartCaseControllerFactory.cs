using System.Web.Mvc;
using System.Web.Routing;
using JetBrains.Annotations;

namespace Portoa.Web.SmartCasing {
	/// <summary>
	/// Controller factory decorator that updates the given controller name using
	/// <see cref="smartCasingConverter"/>
	/// </summary>
	public sealed class SmartCaseControllerFactory : IControllerFactory {
		private readonly IControllerFactory controllerFactory;
		private static readonly SmartCasingConverter smartCasingConverter = new SmartCasingConverter();

		public SmartCaseControllerFactory([NotNull]IControllerFactory controllerFactory) {
			this.controllerFactory = controllerFactory;
		}

		public IController CreateController(RequestContext requestContext, string controllerName) {
			return controllerFactory.CreateController(requestContext, smartCasingConverter.ConvertFrom(controllerName));
		}

		public void ReleaseController(IController controller) {
			controllerFactory.ReleaseController(controller);
		}
	}
}