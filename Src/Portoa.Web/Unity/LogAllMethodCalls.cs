﻿using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Portoa.Logging;
using Portoa.Web.Unity.Matching;

namespace Portoa.Web.Unity {
	/// <summary>
	/// Logs all interceptable method calls via the <see cref="LoggerCallHandler"/>
	/// </summary>
	public class LogAllMethodCalls : UnityContainerExtension {
		protected override void Initialize() {
			Container
				.AddExtensionOnce<Interception>()
				.Configure<Interception>()
				.AddPolicy("LoggingPolicy")
				.AddCallHandler<LoggerCallHandler>(new ContainerControlledLifetimeManager())
				.AddMatchingRule<Not<InstanceOf<ILogger>>>();
		}
	}
}