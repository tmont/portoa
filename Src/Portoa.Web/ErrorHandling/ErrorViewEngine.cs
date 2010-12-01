using System.Web.Mvc;

namespace Portoa.Web.ErrorHandling {
	public class ErrorViewEngine : VirtualPathProviderViewEngine {
		public ErrorViewEngine() {
			ViewLocationFormats = new[] { "~/Views/Error/{0}.aspx" };
			MasterLocationFormats = new[] { "~/Views/Shared/Site.master" };
			PartialViewLocationFormats = new[] { "~/Views/Error/{0}.ascx" };
		}

		protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath) {
			return new WebFormView(partialPath, null);
		}
		
		protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath) {
			return new WebFormView(viewPath, masterPath);
		}

	}
}