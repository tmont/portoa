using System.Net;
using System.Web.Mvc;
using Portoa.Web.Filters;

namespace Portoa.Web.Results {
	public class StatusOverrideActionResult : ActionResult, IStatusOverridable {
		private readonly ActionResult result;

		public StatusOverrideActionResult(ActionResult result) {
			this.result = result;
		}

		public override void ExecuteResult(ControllerContext context) {
			result.ExecuteResult(context);
		}

		public HttpStatusCode StatusCode { get; set; }
	}
}