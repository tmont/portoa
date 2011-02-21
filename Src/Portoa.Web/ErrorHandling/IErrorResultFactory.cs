using System.Net;
using System.Web.Mvc;
using Portoa.Web.Controllers;

namespace Portoa.Web.ErrorHandling {
	/// <summary>
	/// Represents an object that can create error results
	/// </summary>
	/// <seealso cref="DefaultErrorController"/>
	public interface IErrorResultFactory {
		/// <summary>
		/// Creates an <c>ActionResult</c> for error actions
		/// </summary>
		/// <param name="statusCode">The status code for the result</param>
		ActionResult CreateResult(HttpStatusCode statusCode);
	}
}