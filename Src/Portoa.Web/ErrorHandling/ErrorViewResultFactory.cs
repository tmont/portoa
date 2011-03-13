using System.Net;
using System.Web.Mvc;
using Portoa.Web.Results;

namespace Portoa.Web.ErrorHandling {
	/// <summary>
	/// <c cref="IErrorResultFactory">Error result factory</c> that returns a result suitable
	/// for HTML views strongly typed to <c cref="ErrorModel">ErrorModel</c>
	/// </summary>
	/// <see cref="ErrorViewResult"/>
	public sealed class ErrorViewResultFactory : IErrorResultFactory {
		public ActionResult CreateResult(HttpStatusCode statusCode) {
			return new CompositeResult {
				new HttpStatusCodeResult((int)statusCode),
				new ErrorViewResult { ModelCreator = exception => new ErrorModel { Exception = exception } }
			};
		}
	}
}