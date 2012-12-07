using Pandora.Fluent;
using dotless.Core;
using dotless.Core.Input;
using dotless.Core.Loggers;
using dotless.Core.configuration;

namespace Portoa.Less {
	/// <summary>
	/// Overrides some of the default service registrations in dotLess
	/// so that you can inject your own logger and path resolve
	/// </summary>
	public class OverriddenContainerFactory : ContainerFactory {
		private readonly ILogger logger;
		private readonly IPathResolver pathResolver;

		public OverriddenContainerFactory(ILogger logger, IPathResolver pathResolver) {
			this.logger = logger;
			this.pathResolver = pathResolver;
		}

		protected override void OverrideServices(FluentRegistration pandora, DotlessConfiguration configuration) {
			pandora.Service<ILogger>().Instance(logger);
			pandora.Service<IPathResolver>().Instance(pathResolver);
		}
	}
}