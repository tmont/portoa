using JetBrains.Annotations;
using Microsoft.Practices.Unity;
using Portoa.Security;
using Portoa.Web.ErrorHandling;
using Portoa.Web.Unity.UnitOfWork;

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
	/// Convenience base class for an MVC application using Unity/NHibernate
	/// </summary>
	public abstract class NHibernateDrivenMvcApplication : MvcApplicationBase {
		protected NHibernateDrivenMvcApplication() {
			Extensions.Add(new EnableNHibernate());
		}
	}
}