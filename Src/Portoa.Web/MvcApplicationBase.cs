﻿using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using NHibernate;
using NHibernate.Cfg;
using Portoa.Logging;
using Portoa.NHibernate;
using Portoa.Persistence;
using Portoa.Web.Controllers;
using Portoa.Web.ErrorHandling;
using Portoa.Web.Filters;
using Portoa.Web.Security;
using Portoa.Web.Session;
using Portoa.Web.Unity;
using Portoa.Web.Unity.Lifetime;

namespace Portoa.Web {
	/// <summary>
	/// Base for an MVC application using Unity/NHibernate
	/// </summary>
	/// <typeparam name="T">The user type</typeparam>
	public abstract class MvcApplicationBase<T> : HttpApplication where T : class {
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

		private static T GetCurrentUser() {
			return Container.IsRegistered<ICurrentUserProvider<T>>()
				? Container.Resolve<ICurrentUserProvider<T>>().CurrentUser
				: null;
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
				.HandleError(exception, new DefaultErrorController(new ErrorWithUserResultFactory<T>(GetCurrentUser())));
		}

		protected void Application_Start() {
			Container
				.AddNewExtension<Interception>()
				.AddNewExtension<ApplyUnityConfigurationSection>()
				.AddNewExtension<ConfigureUnitOfWorkAspect>()
				.RegisterType<Configuration>(new ContainerControlledLifetimeManager(), new InjectionFactory(CreateNHibernateConfiguration))
				.RegisterType<IUnitOfWork, NHibernateUnitOfWork>()
				.RegisterType<IIdentity>(new PerRequestLifetimeManager(), new InjectionFactory(container => container.Resolve<HttpContextBase>().User.Identity))
				.RegisterType<IServiceProvider, ContainerResolvingServiceProvider>(new ContainerControlledLifetimeManager())
				.RegisterAndIntercept(typeof(IRepository<>), typeof(NHibernateRepository<>))
				.RegisterAndIntercept(typeof(IRepository<,>), typeof(NHibernateRepository<,>))
				.RegisterAndIntercept<ISessionStore, HttpSessionStore>(new PerRequestLifetimeManager())
				.RegisterType<ISessionFactory>(new PerRequestLifetimeManager(), new InjectionFactory(container => container.Resolve<Configuration>().BuildSessionFactory()))
				.RegisterType<ISession>(new PerRequestLifetimeManager(), new InjectionFactory(container => container.Resolve<ISessionFactory>().OpenSession()))
				.RegisterType<HttpContextBase>(new PerRequestLifetimeManager(), new InjectionFactory(c => new HttpContextWrapper(HttpContext.Current)));

			ConfigureUnity();

			if (!Container.IsRegistered<ILogger>()) {
				Container.RegisterType<ILogger, NullLogger>();
			}

			ConfigureControllerFactory();
			ConfigureModelBinders(ModelBinders.Binders);
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
		/// Creates the configuration used for NHibernate; default implementation uses the default
		/// NHibernate configuration, and sets a custom interceptor. The configuration can be retrieved
		/// outside of this method (if needed) via the container. Do not call this method directly.
		/// </summary>
		/// <seealso cref="BuildWithProviderInterceptor"/>
		protected virtual Configuration CreateNHibernateConfiguration(IUnityContainer container) {
			var cfg = new Configuration().Configure();
			cfg.SetInterceptor(new BuildWithProviderInterceptor(container.Resolve<IServiceProvider>(), cfg.ClassMappings));
			return cfg;
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