using System.Net;
using System.Web.Mvc;

namespace Portoa.Web.ErrorHandling {
	/// <summary>
	/// <c cref="IErrorResultFactory">Error result factory</c> that returns a result suitable
	/// for HTML views strongly typed to <c cref="ErrorModel">ErrorModel</c>
	/// </summary>
	/// <see cref="ErrorViewResult"/>
	public sealed class ErrorViewResultFactory : IErrorResultFactory {
		public ActionResult CreateResult(HttpStatusCode statusCode) {
			return new ErrorViewResult { StatusCode = statusCode, ModelCreator = exception => new ErrorModel { Exception = exception } };
		}
	}
}