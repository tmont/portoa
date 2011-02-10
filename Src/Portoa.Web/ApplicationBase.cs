using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using JetBrains.Annotations;
using Microsoft.Practices.Unity;
using NHibernate;
using NHibernate.Cfg;
using Portoa.Logging;
using Portoa.NHibernate;
using Portoa.Persistence;
using Portoa.Web.Controllers;
using Portoa.Web.ErrorHandling;
using Portoa.Web.Filters;
using Portoa.Web.Session;
using Portoa.Web.Unity;

namespace Portoa.Web {
	/// <summary>
	/// Base for an Unity/NHibernate MVC application
	/// </summary>
	public abstract class ApplicationBase : HttpApplication {
		protected static readonly IUnityContainer Container = new UnityContainer();

		protected ApplicationBase() {
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
				if (!Container.IsRegistered<ISessionFactory>()) {
					return;
				}

				if (Container.IsRegistered<ILogger>()) {
					var logger = Container.Resolve<ILogger>();
					if (logger.IsDebugEnabled) {
						logger.Debug(new string('-', 20));
					}
				}

				Container.Resolve<ISessionFactory>().Dispose();
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

			new ApplicationErrorHandler(Container.Resolve<ILogger>(), Container.Resolve<HttpContextBase>()).HandleError(exception, new DefaultErrorController());
		}

		[UsedImplicitly]
		private void Application_Start() {
			var cfg = new Configuration().Configure();
			cfg.SetInterceptor(new BuildWithProviderInterceptor(Container.Resolve<IServiceProvider>(), cfg.ClassMappings));

			Container
				.AddNewExtension<ApplyUnityConfigurationSection>()
				.AddNewExtension<ConfigureUnitOfWorkAspect>()
				.RegisterType<IUnitOfWork, NHibernateUnitOfWork>()
				.RegisterType<IIdentity>(new PerRequestLifetimeManager(), new InjectionFactory(container => container.Resolve<HttpContextBase>().User.Identity))
				.RegisterInstance<IServiceProvider>(new ContainerResolvingServiceProvider(Container), new ContainerControlledLifetimeManager())
				.RegisterType(typeof(IRepository<>), typeof(NHibernateRepository<>))
				.RegisterType(typeof(IRepository<,>), typeof(NHibernateRepository<,>))
				.RegisterType<ISessionStore, HttpSessionStore>(new PerRequestLifetimeManager())
				.RegisterType<ISessionFactory>(new PerRequestLifetimeManager(), new InjectionFactory(container => cfg.BuildSessionFactory()))
				.RegisterType<ISession>(new PerRequestLifetimeManager(), new InjectionFactory(container => container.Resolve<ISessionFactory>().OpenSession()))
				.RegisterType<HttpContextBase>(new PerRequestLifetimeManager(), new InjectionFactory(c => new HttpContextWrapper(HttpContext.Current)));

			ConfigureUnity();
			ConfigureControllerFactory();
			RegisterAreas();
			RegisterRoutes(RouteTable.Routes);
		}

		/// <summary>
		/// Registers any routes for the application; default implementation registers nothing
		/// </summary>
		protected virtual void RegisterRoutes(RouteCollection routes) { }

		/// <summary>
		/// Registers any applicable areas; default implementations calls
		/// <c>AreaRegistration.RegisterAllAreas()</c>
		/// </summary>
		protected virtual void RegisterAreas() {
			AreaRegistration.RegisterAllAreas();
		}

		/// <summary>
		/// Configures the controller factory; default implementation uses Unity to
		/// resolve each controller with a custom action invoker
		/// </summary>
		/// <see cref="InjectableFilterActionInvoker"/>
		protected virtual void ConfigureControllerFactory() {
			var actionInvoker = new InjectableFilterActionInvoker(Container)
				.AddAuthorizationFilter(new ValidateInputAttribute(false))
				.AddResultFilter<OverrideStatusCodeFilter>();

			var controllerFactory = new InjectableControllerFactory(Container);
			controllerFactory.OnControllerInstantiated += controllerImpl => {
				var controller = controllerImpl as Controller;
				if (controller == null) {
					return;
				}

				controller.ActionInvoker = actionInvoker;
			};

			ControllerBuilder.Current.SetControllerFactory(controllerFactory);
		}

		/// <summary>
		/// Performs any application-specific configuration for Unity; default implementation
		/// does nothing
		/// </summary>
		protected virtual void ConfigureUnity() { }

		private class InjectableControllerFactory : ContainerControllerFactory, IInjectableControllerFactory {
			public InjectableControllerFactory(IUnityContainer container) : base(container) { }
			public event Action<IController> OnControllerInstantiated;

			protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType) {
				var controller = base.GetControllerInstance(requestContext, controllerType);

				if (OnControllerInstantiated != null) {
					OnControllerInstantiated.Invoke(controller);
				}

				return controller;
			}
		}
	}
}