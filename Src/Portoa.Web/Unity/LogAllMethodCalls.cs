using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Portoa.Logging;

namespace Portoa.Web.Unity {
	public class LogAllMethodCalls : UnityContainerExtension {
		protected override void Initialize() {
			Container
				.AddExtensionOnce<Interception>()
				.Configure<Interception>()
				.AddPolicy("LoggingPolicy")
				.AddCallHandler<LoggerCallHandler>()
				.AddMatchingRule<AlwaysApply>()
				.AddMatchingRule<NotInstanceOf<ILogger>>();
		}
	}
}