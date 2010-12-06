using System.Web.Mvc;

namespace Portoa.Web.Controllers {
	/// <summary>
	/// Signifies that this controller can handle errors that aren't handled by
	/// application code
	/// </summary>
	public interface IErrorController : IController {
		/// <summary>
		/// Executed when an unknown error occurs
		/// </summary>
		ActionResult Unknown();
		/// <summary>
		/// Executed when a page is not found
		/// </summary>
		ActionResult NotFound();
		/// <summary>
		/// Executed when the user is forbidden from seeing the requested page
		/// </summary>
		ActionResult Forbidden();
	}
}