using System;
using Microsoft.Practices.Unity;
using NHibernate;
using NHibernate.Cfg;
using Portoa.NHibernate;
using Portoa.Persistence;
using Portoa.Web.Unity.Lifetime;

namespace Portoa.Web.Unity.UnitOfWork {
	/// <summary>
	/// Registers everything needed to use NHibernate with Unity
	/// </summary>
	/// <seealso cref="EnableUnitOfWork"/>
	public class EnableNHibernate : UnityContainerExtension {
		private readonly Func<IUnityContainer, Configuration> configFactory;

		/// <param name="configFactory">Optional factory delegate for creating the NHibernate configuration</param>
		public EnableNHibernate(Func<IUnityContainer, Configuration> configFactory = null) {
			this.configFactory = configFactory;
		}

		protected override void Initialize() {
			Container
				.AddNewExtension<EnableUnitOfWork>()
				.RegisterType<Configuration>(new ContainerControlledLifetimeManager(), new InjectionFactory(configFactory ?? CreateNHibernateConfiguration))
				.RegisterType<IUnitOfWork, NHibernateUnitOfWork>()
				.RegisterAndIntercept(typeof(IRepository<>), typeof(NHibernateRepository<>))
				.RegisterType<ISessionFactory>(new ContainerControlledLifetimeManager(), new InjectionFactory(container => container.Resolve<Configuration>().BuildSessionFactory()))
				.RegisterType<ISession>(new PerRequestLifetimeManager(), new InjectionFactory(container => container.Resolve<ISessionFactory>().OpenSession()));
		}

		private static Configuration CreateNHibernateConfiguration(IUnityContainer container) {
			var cfg = new Configuration().Configure();
			cfg.SetInterceptor(new BuildWithProviderInterceptor(container.Resolve<IServiceProvider>(), cfg.ClassMappings));
			return cfg;
		}
	}
}