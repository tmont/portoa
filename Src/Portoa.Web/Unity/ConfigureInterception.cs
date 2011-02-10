using Microsoft.Practices.Unity;
using NHibernate;

namespace Portoa.Web.Unity {
	/// <summary>
	/// Configures interception to occur for any subsequent container registrations
	/// that are not part of NHibernate
	/// </summary>
	public class ConfigureInterception : UnityContainerExtension {
		protected override void Initialize() {
			Container.AddExtension(new InterceptOnRegister().AddNewMatchingRule<NotAssemblyOf<ISession>>());
		}
	}
}