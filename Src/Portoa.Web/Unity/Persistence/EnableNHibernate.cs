using System;
using Microsoft.Practices.Unity;
using NHibernate;
using NHibernate.Cfg;
using Portoa.NHibernate;
using Portoa.Persistence;
using Portoa.Web.Unity.Lifetime;

namespace Portoa.Web.Unity.Persistence {

	/// <summary>
	/// Registers everything needed to use NHibernate with Unity
	/// </summary>
	/// <seealso cref="EnableUnitOfWork"/>
	public class EnableNHibernate : UnityContainerExtension {
		protected override void Initialize() {
			Container
				.AddNewExtension<EnableUnitOfWork>()
				.RegisterType<Configuration>(new ContainerControlledLifetimeManager(), new InjectionFactory(CreateNHibernateConfiguration))
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