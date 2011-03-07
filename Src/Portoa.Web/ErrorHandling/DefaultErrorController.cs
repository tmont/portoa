using System.Net;
using System.Web.Mvc;

namespace Portoa.Web.ErrorHandling {
	/// <summary>
	/// Controller for handling and displaying errors that aren't handled by application code
	/// </summary>
	public class DefaultErrorController : Controller, IErrorController {
		private readonly IErrorResultFactory errorResultFactory;

		/// <param name="errorResultFactory">The factory to use to create the action results</param>
		public DefaultErrorController(IErrorResultFactory errorResultFactory) {
			this.errorResultFactory = errorResultFactory;
		}

		public ActionResult Unknown() {
			return errorResultFactory.CreateResult(HttpStatusCode.InternalServerError);
		}

		public ActionResult NotFound() {
			return errorResultFactory.CreateResult(HttpStatusCode.NotFound);
		}

		public ActionResult Forbidden() {
			return errorResultFactory.CreateResult(HttpStatusCode.Forbidden);
		}
	}
}