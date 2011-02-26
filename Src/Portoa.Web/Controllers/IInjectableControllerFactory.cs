using System;
using System.Web.Mvc;

namespace Portoa.Web.Controllers {
	/// <summary>
	/// <c cref="IControllerFactory">IControllerFactory</c> that provides a mechanism to perform injection
	/// on the controller after it is instantiated
	/// </summary>
	public interface IInjectableControllerFactory : IControllerFactory {
		/// <summary>
		/// Event that fires after a controller is instantiated
		/// </summary>
		event Action<IController> OnControllerInstantiated;
	}
}