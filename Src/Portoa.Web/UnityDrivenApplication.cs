﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using JetBrains.Annotations;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.Practices.Unity.InterceptionExtension;
using Portoa.Logging;
using Portoa.Security;
using Portoa.Util;
using Portoa.Web.Controllers;
using Portoa.Web.ErrorHandling;
using Portoa.Web.Filters;
using Portoa.Web.Session;
using Portoa.Web.SmartCasing;
using Portoa.Web.Unity;
using Portoa.Web.Unity.Lifetime;

namespace Portoa.Web {

	/// <summary>
	/// Base for an MVC application using Unity
	/// </summary>
	public abstract class UnityDrivenApplication : HttpApplication {
		/// <summary>
		/// The container associated with this application
		/// </summary>
		protected static readonly IUnityContainer Container = new UnityContainer();

		/// <summary>
		/// Extensions to register with the <see cref="Container"/>
		/// </summary>
		protected static readonly ISet<UnityContainerExtension> Extensions = new HashSet<UnityContainerExtension>();

		protected UnityDrivenApplication() {
			var startTime = 0;
			BeginRequest += (sender, args) => {
				startTime = DateTime.UtcNow.ToUnixTimestamp();
				if (!Container.IsRegistered<ILogger>()) {
					return;
				}

				var logger = Container.Resolve<ILogger>();
				if (logger.IsDebugEnabled) {
					logger.Debug("{0} {1}", Context.Request.HttpMethod, Context.Request.RawUrl);
				}
			};

			EndRequest += (sender, args) => {
				var requestDuration = DateTime.UtcNow.ToUnixTimestamp() - startTime;
				if (Container.IsRegistered<ILogger>()) {
					var logger = Container.Resolve<ILogger>();
					if (logger.IsDebugEnabled) {
						var message = " " + requestDuration + "ms ";
						logger.Debug(new string('-', 15) + message + new string('-', 15 - message.Length));
					}
				}

				OnRequestEnded();
			};

			Error += (sender, args) => {
				var exception = Server.GetLastError();

				Server.ClearError();
				HandleApplicationError(exception);
			};
		}

		/// <summary>
		/// Performs actions at the end of each request; default implementation
		/// does nothing
		/// </summary>
		protected virtual void OnRequestEnded() { }

		/// <summary>
		/// Handles uncaught application exceptions; default implementation uses <see cref="ApplicationErrorHandler"/>
		/// </summary>
		/// <param name="exception">The uncaught exception</param>
		protected virtual void HandleApplicationError(Exception exception) {
			//safely resolving these so that no resolution exceptions are thrown
			var errorResultFactory = Container.TryResolve<IErrorResultFactory>() ?? new ErrorViewResultFactory();
			var errorController = Container.TryResolve<IErrorController>() ?? new DefaultErrorController(errorResultFactory);

			new ApplicationErrorHandler(Container.TryResolve<ILogger>(), Container.TryResolve<HttpContextBase>())
				.HandleError(exception, errorController);
		}

		protected void Application_Start() {
			ConfigureErrorHandlers();

			Container
				.AddNewExtension<Interception>()
				.RegisterType<UnityConfigurationSection>(new ContainerControlledLifetimeManager(), new InjectionFactory(GetUnityConfigurationSection))
				.AddNewExtension<ConfigureUsingAppConfig>()
				.RegisterType<IIdentity>(new PerRequestLifetimeManager(), new InjectionFactory(container => container.Resolve<HttpContextBase>().User.Identity))
				.RegisterType<IServiceProvider, ContainerResolvingServiceProvider>(new ContainerControlledLifetimeManager())
				.RegisterAndIntercept<ISessionStore, HttpSessionStore>(new PerRequestLifetimeManager())
				.RegisterType<HttpContextBase>(new PerRequestLifetimeManager(), new InjectionFactory(c => new HttpContextWrapper(HttpContext.Current)));

			foreach (var extension in Extensions) {
				Container.AddExtension(extension);
			}

			if (!Container.IsRegistered<ILogger>()) {
				Container.RegisterType<ILogger, NullLogger>();
			}

			ViewEngines.Engines.Clear();
			FilterProviders.Providers.Clear();

			SetControllerFactory();
			RegisterModelBinders(ModelBinders.Binders);
			RegisterFilterProviders(FilterProviders.Providers);
			RegisterViewEngines(ViewEngines.Engines);
			RegisterAreas();
			RegisterRoutes(RouteTable.Routes);
			RegisterGlobalFilters();
			AfterStartUp();
		}

		/// <summary>
		/// Gets Unity's configuration section from the app config
		/// </summary>
		/// <returns>Unity's configuration section, or <c>null</c> if no section exists</returns>
		protected virtual UnityConfigurationSection GetUnityConfigurationSection(IUnityContainer container) {
			return ConfigurationManager.GetSection("unity") as UnityConfigurationSection;
		}

		/// <summary>
		/// Configures the error handling objects. This happens before anything else. Default
		/// implementation registers <see cref="ErrorViewResultFactory"/> and <see cref="DefaultErrorController"/>
		/// with the <see cref="Container"/>
		/// </summary>
		protected virtual void ConfigureErrorHandlers() {
			Container
				.RegisterType<IErrorResultFactory, ErrorViewResultFactory>()
				.RegisterType<IErrorController, DefaultErrorController>();
		}

		/// <summary>
		/// Override to perform any extra startup tasks; default implementation does nothing
		/// </summary>
		protected virtual void AfterStartUp() { }

		/// <summary>
		/// Override to configure the model binders for the application; default implementation
		/// does nothing (the binders in the collection are the default MVC binders)
		/// </summary>
		protected virtual void RegisterModelBinders(ModelBinderDictionary binders) { }

		/// <summary>
		/// Registers any routes for the application; default implementation registers nothing
		/// </summary>
		protected virtual void RegisterRoutes(RouteCollection routes) { }

		/// <summary>
		/// Registers view engines; defualt implementation only registers <see cref="RazorViewEngine"/>
		/// and <see cref="SmartCaseViewEngine"/> if <see cref="ShouldEnableSmartCasing"/> is <c>true</c>
		/// </summary>
		protected virtual void RegisterViewEngines(ViewEngineCollection engines) {
			engines.Add(new RazorViewEngine());
			if (ShouldEnableSmartCasing) {
				ViewEngines.Engines.Add(new SmartCaseViewEngine(Container.Resolve<ILogger>()));
			}
		}

		/// <summary>
		/// Registers any applicable areas; default implementations calls
		/// <see cref="AreaRegistration.RegisterAllAreas()"/>
		/// </summary>
		protected virtual void RegisterAreas() {
			AreaRegistration.RegisterAllAreas();
		}

		/// <summary>
		/// Registers global filters; default adds a filter to disable validation input
		/// </summary>
		protected virtual void RegisterGlobalFilters() {
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

		/// <summary>
		/// Sets up the filter providers; default implementation uses <see cref="AdjustableFilterProvider"/>
		/// to build up each filter instance using the <see cref="Container"/>
		/// </summary>
		protected virtual void RegisterFilterProviders(FilterProviderCollection providers) {
			FilterProviders.Providers.Add(new AdjustableFilterProvider(new InjectionFilterAdjuster(Container)));
		}

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

	/// <summary>
	/// Base for an MVC application that supports a userbase
	/// </summary>
	/// <typeparam name="T">The user type</typeparam>
	public abstract class UnityDrivenApplication<T> : UnityDrivenApplication where T : class {
		[CanBeNull]
		private static T GetCurrentUser() {
			return Container.IsRegistered<ICurrentUserProvider<T>>()
				? Container.Resolve<ICurrentUserProvider<T>>().CurrentUser
				: default(T);
		}

		protected override void ConfigureErrorHandlers() {
			Container.RegisterType<IErrorResultFactory, ErrorWithUserResultFactory<T>>(
				new InjectionFactory(container => new ErrorWithUserResultFactory<T>(GetCurrentUser()))
			);
		}
	}
}