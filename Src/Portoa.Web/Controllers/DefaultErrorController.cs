using System;
using System.Net;
using System.Web.Mvc;
using Portoa.Web.ErrorHandling;

namespace Portoa.Web.Controllers {
	/// <summary>
	/// Controller for handling and displaying errors that aren't handled by application code
	/// </summary>
	public class DefaultErrorController : Controller, IErrorController {
		private static object CreateModel(Exception exception) {
			return new ErrorModel { Exception = exception };
		}

		private static ErrorViewResult CreateResult(HttpStatusCode statusCode) {
			return new ErrorViewResult { StatusCode = statusCode, ModelCreator = CreateModel };
		}

		public ActionResult Unknown() {
			return CreateResult(HttpStatusCode.InternalServerError);
		}

		public ActionResult NotFound() {
			return CreateResult(HttpStatusCode.NotFound);
		}

		public ActionResult Forbidden() {
			return CreateResult(HttpStatusCode.Forbidden);
		}
	}
}