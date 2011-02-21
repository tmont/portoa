using System.Net;
using System.Web.Mvc;
using JetBrains.Annotations;

namespace Portoa.Web.ErrorHandling {
	/// <summary>
	/// <c cref="IErrorResultFactory">Error result factory</c> that returns a result suitable
	/// for HTML views strongly typed to <c cref="ErrorModel{T}">ErrorModel&lt;T&gt;</c>
	/// </summary>
	/// <see cref="ErrorViewResult"/>
	/// <typeparam name="T">The user type</typeparam>
	public sealed class ErrorWithUserResultFactory<T> : IErrorResultFactory {
		private readonly T user;

		/// <param name="user">The currently logged in user</param>
		public ErrorWithUserResultFactory([CanBeNull]T user) {
			this.user = user;
		}

		public ActionResult CreateResult(HttpStatusCode statusCode) {
			return new ErrorViewResult { 
				StatusCode = statusCode,
				ModelCreator = exception => new ErrorModel<T> { Exception = exception, User = user } 
			};
		}
	}
}