using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Portoa.Logging;
using Portoa.Web.Unity.Matching;

namespace Portoa.Web.Unity.Logging {
	/// <summary>
	/// Logs all interceptable method calls via the <see cref="MethodLoggingCallHandler"/>
	/// </summary>
	[DependsOnExtensions(typeof(Interception))]
	public class LogAllMethodCalls : VerifiableContainerExtension {
		protected override void DoInitialize() {
			Container
				.Configure<Interception>()
				.AddPolicy("LoggingPolicy")
				.AddCallHandler<MethodLoggingCallHandler>(new ContainerControlledLifetimeManager())
				.AddMatchingRule<Not<InstanceOf<ILogger>>>();
		}
	}
}