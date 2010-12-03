using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Portoa.Persistence;

namespace Portoa.Web.Unity {
	public class ConfigureUnitOfWorkAspect : UnityContainerExtension {
		protected override void Initialize() {
			Container
				.AddNewExtension<Interception>()
				.Configure<Interception>()
				.AddPolicy("UnitOfWorkPolicy")
				.AddCallHandler<UnitOfWorkCallHandler>()
				.AddMatchingRule<HasAttribute<UnitOfWorkAttribute>>();
		}
	}
}