using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Portoa.Logging;
using Portoa.Web.Controllers;
using Portoa.Web.ErrorHandling;
using Portoa.Web.Session;
using Portoa.Web.SmartCasing;
using Portoa.Web.Unity;
using Portoa.Web.Unity.Lifetime;

namespace Portoa.Web {

	/// <summary>
	/// Base for an MVC application using Unity
	/// </summary>
	public abstract class MvcApplicationBase : HttpApplication {
		/// <summary>
		/// The container associated with this application
		/// </summary>
		protected static readonly IUnityContainer Container = new UnityContainer();

		protected MvcApplicationBase() {
			BeginRequest += (sender, args) => {
				if (!Container.IsRegistered<ILogger>()) {
					return;
				}

				var logger = Container.Resolve<ILogger>();
				if (logger.IsDebugEnabled) {
					logger.Debug("{0} {1}", Context.Request.HttpMethod, Context.Request.RawUrl);
				}
			};

			EndRequest += (sender, args) => {
				if (Container.IsRegistered<ILogger>()) {
					var logger = Container.Resolve<ILogger>();
					if (logger.IsDebugEnabled) {
						logger.Debug(new string('-', 20));
					}
				}
			};

			Error += (sender, args) => {
				var exception = Server.GetLastError();

				Server.ClearError();
				HandleApplicationError(exception);
			};
		}

		/// <summary>
		/// Handles uncaught application exceptions; default implementation uses
		/// <see cref="ApplicationErrorHandler"/> and <see cref="DefaultErrorController"/> to
		/// display errors
		/// </summary>
		/// <param name="exception">The uncaught exception</param>
		protected virtual void HandleApplicationError(Exception exception) {
			if (!Container.AllAreRegistered(typeof(ILogger), typeof(HttpContextBase))) {
				throw exception;
			}

			new ApplicationErrorHandler(Container.Resolve<ILogger>(), Container.Resolve<HttpContextBase>())
				.HandleError(exception, new DefaultErrorController(new ErrorViewResultFactory()));
		}

		protected void Application_Start() {
			Container
				.AddNewExtension<Interception>()
				.AddNewExtension<ApplyUnityConfigurationSection>()
				.RegisterType<IIdentity>(new PerRequestLifetimeManager(), new InjectionFactory(container => container.Resolve<HttpContextBase>().User.Identity))
				.RegisterType<IServiceProvider, ContainerResolvingServiceProvider>(new ContainerControlledLifetimeManager())
				.RegisterAndIntercept<ISessionStore, HttpSessionStore>(new PerRequestLifetimeManager())
				.RegisterType<HttpContextBase>(new PerRequestLifetimeManager(), new InjectionFactory(c => new HttpContextWrapper(HttpContext.Current)));

			ConfigureUnity();

			if (!Container.IsRegistered<ILogger>()) {
				Container.RegisterType<ILogger, NullLogger>();
			}

			SetControllerFactory();

			if (ShouldEnableSmartCasing) {
				EnableSmartCasing();
			}

			ConfigureModelBinders(ModelBinders.Binders);
			ViewEngines.Engines.Clear();
			RegisterViewEngines(ViewEngines.Engines);
			RegisterAreas();
			RegisterRoutes(RouteTable.Routes);
			AfterStartUp();
		}

		/// <summary>
		/// Override to perform any extra startup tasks; default implementation does nothing
		/// </summary>
		protected virtual void AfterStartUp() { }

		/// <summary>
		/// Override to configure the model binders for the application; default implementation
		/// does nothing
		/// </summary>
		protected virtual void ConfigureModelBinders(ModelBinderDictionary binders) { }

		/// <summary>
		/// Registers any routes for the application; default implementation registers nothing
		/// </summary>
		protected virtual void RegisterRoutes(RouteCollection routes) { }

		/// <summary>
		/// Registers view engines; defualt implementation only registers <see cref="RazorViewEngine"/>
		/// </summary>
		protected virtual void RegisterViewEngines(ViewEngineCollection engines) {
			engines.Add(new RazorViewEngine());
		}

		/// <summary>
		/// Registers any applicable areas; default implementations calls
		/// <c>AreaRegistration.RegisterAllAreas()</c>
		/// </summary>
		protected virtual void RegisterAreas() {
			AreaRegistration.RegisterAllAreas();
		}

		/// <summary>
		/// Registers global filters; default adds a filter to disable validation input
		/// </summary>
		protected virtual void RegisterGlobalFilters(GlobalFilterCollection filters) {
			GlobalFilters.Filters.Add(new ValidateInputAttribute(false));
		}

		/// <summary>
		/// Configures the controller factory; default implementation uses Unity to
		/// resolve each controller
		/// </summary>
		protected virtual void SetControllerFactory() {
			ControllerBuilder.Current.SetControllerFactory(new ContainerControllerFactory(Container));
		}

		/// <summary>
		/// Gets whether <c cref="SmartCaseConverter">smart casing</c> should be enabled.
		/// Smart casing uses the <c cref="RazorViewEngine">Razor</c> view engine.
		/// </summary>
		/// <seealso cref="SmartCaseConverter"/>
		/// <seealso cref="RouteExtensions.MapSmartRoute"/>
		/// <seealso cref="SmartCaseViewEngine"/>
		/// <seealso cref="SmartCaseRouteHandler"/>
		protected virtual bool ShouldEnableSmartCasing { get { return false; } }

		private static void EnableSmartCasing() {
			var logger = Container.IsRegistered<ILogger>() ? Container.Resolve<ILogger>() : new NullLogger();
			ViewEngines.Engines.Add(new SmartCaseViewEngine(logger));
		}

		/// <summary>
		/// Performs any application-specific configuration for Unity; default implementation
		/// does nothing
		/// </summary>
		protected virtual void ConfigureUnity() { }

		protected void Application_End() {
			OnApplicationEnd();

			if (Container != null) {
				Container.Dispose();
			}
		}

		/// <summary>
		/// Performs any needed cleanup when the application ends; default implementation
		/// does nothing. Be aware that the <c cref="Container">container</c> gets disposed of
		/// later.
		/// </summary>
		protected virtual void OnApplicationEnd() { }
	}
}