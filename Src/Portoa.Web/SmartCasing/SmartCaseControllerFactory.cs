using System.Web.Mvc;
using System.Web.Routing;
using JetBrains.Annotations;

namespace Portoa.Web.SmartCasing {
	/// <summary>
	/// Controller factory decorator that updates the given controller name using
	/// <see cref="smartCaseConverter"/>
	/// </summary>
	public sealed class SmartCaseControllerFactory : IControllerFactory {
		private readonly IControllerFactory controllerFactory;
		private static readonly SmartCaseConverter smartCaseConverter = new SmartCaseConverter();

		public SmartCaseControllerFactory([NotNull]IControllerFactory controllerFactory) {
			this.controllerFactory = controllerFactory;
		}

		public IController CreateController(RequestContext requestContext, string controllerName) {
			return controllerFactory.CreateController(requestContext, smartCaseConverter.ConvertFrom(controllerName));
		}

		public void ReleaseController(IController controller) {
			controllerFactory.ReleaseController(controller);
		}
	}
}