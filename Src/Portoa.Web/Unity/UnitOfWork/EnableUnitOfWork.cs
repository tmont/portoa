using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Portoa.Persistence;
using Portoa.Web.Unity.Matching;

namespace Portoa.Web.Unity.UnitOfWork {
	/// <summary>
	/// Enables the use of the <see cref="UnitOfWorkAttribute"/> to perform
	/// transactions around method calls
	/// </summary>
	[DependsOnExtensions(typeof(Interception))]
	public class EnableUnitOfWork : VerifiableContainerExtension {
		protected override void DoInitialize() {
			Container
				.Configure<Interception>()
				.AddPolicy("UnitOfWorkPolicy")
				.AddCallHandler<UnitOfWorkCallHandler>()
				.AddMatchingRule<HasAttribute<UnitOfWorkAttribute>>();
		}
	}
}