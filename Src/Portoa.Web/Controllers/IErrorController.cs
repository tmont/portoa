using System.Web.Mvc;

namespace Portoa.Web.Controllers {
	public interface IErrorController : IController {
		ActionResult Unknown();
		ActionResult NotFound();
		ActionResult Forbidden();
	}
}