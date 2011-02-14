using System.Net;
using System.Web.Mvc;
using Portoa.Web.Filters;

namespace Portoa.Web.Results {
	/// <summary>
	/// <c>ActionResult</c> decorator that enables you to override the HTTP status code
	/// </summary>
	/// <see cref="OverrideStatusCodeFilter"/>
	public class StatusOverrideResult : ActionResult, IStatusOverridable {
		private readonly ActionResult result;

		public StatusOverrideResult(ActionResult result) {
			this.result = result;
		}

		public override void ExecuteResult(ControllerContext context) {
			result.ExecuteResult(context);
		}

		public HttpStatusCode StatusCode { get; set; }
	}
}