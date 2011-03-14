using System;
using JetBrains.Annotations;
using Microsoft.Practices.Unity;
using NHibernate;
using NHibernate.Cfg;
using Portoa.NHibernate;
using Portoa.Persistence;
using Portoa.Web.ErrorHandling;
using Portoa.Web.Security;
using Portoa.Web.Unity;
using Portoa.Web.Unity.Lifetime;

namespace Portoa.Web {

	/// <summary>
	/// Base for an MVC application using Unity/NHibernate that supports a userbase
	/// </summary>
	/// <typeparam name="T">The user type</typeparam>
	public abstract class NHibernateDrivenMvcApplication<T> : NHibernateDrivenMvcApplication where T : class {
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

	/// <summary>
	/// Base for an MVC application using Unity/NHibernate
	/// </summary>
	public abstract class NHibernateDrivenMvcApplication : MvcApplicationBase {
		protected override sealed void ConfigureUnity() {
			Container
				.AddNewExtension<ConfigureUnitOfWorkAspect>()
				.RegisterType<Configuration>(new ContainerControlledLifetimeManager(), new InjectionFactory(CreateNHibernateConfiguration))
				.RegisterType<IUnitOfWork, NHibernateUnitOfWork>()
				.RegisterAndIntercept(typeof(IRepository<,>), typeof(NHibernateRepository<,>))
				.RegisterType<ISessionFactory>(new PerRequestLifetimeManager(), new InjectionFactory(container => container.Resolve<Configuration>().BuildSessionFactory()))
				.RegisterType<ISession>(new PerRequestLifetimeManager(), new InjectionFactory(container => container.Resolve<ISessionFactory>().OpenSession()));

			ConfigureUnityForApplication();
		}

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
		/// Provides a place to perform any application-specific configuration; default implementation
		/// does nothing
		/// </summary>
		protected virtual void ConfigureUnityForApplication() { }
	}
}