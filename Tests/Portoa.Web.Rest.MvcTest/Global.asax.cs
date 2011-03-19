using System.Web.Mvc;
using System.Web.Routing;
using Portoa.Web.Rest.MvcTest.Controllers;
using Microsoft.Practices.Unity;
using Portoa.Web.Rest.Parser;
using Portoa.Web.Models;

namespace Portoa.Web.Rest.MvcTest {
	public class MvcApplication : NHibernateDrivenMvcApplication {

		protected override void RegisterModelBinders(ModelBinderDictionary binders) {
			binders.Add<RestRequest, RestRequestModelBinder>(Container);
		}

		protected override void RegisterRoutes(RouteCollection routes) {
			routes.MapRoute("rest", "user/{id}", new { controller = "Rest", action = "ListUsers", id = "all" }, new { id = @"\d+|all" });
			routes.MapRoute("Default", "", new { controller = "Rest", action = "Index" });
		}

		protected override void ConfigureUnityForApplication() {
			Container
				.RegisterType<ICriterionParserFactory, DefaultCriterionParserFactory>()
				.RegisterType<IRestIdParser, IdentityIdParser>()
				.RegisterType<IRestService, RestService>();
		}
	}
}