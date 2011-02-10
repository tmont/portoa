using System;
using log4net;
using log4net.Config;
using Microsoft.Practices.Unity;
using Portoa.Log4Net;
using Portoa.Logging;

namespace Portoa.Web.Unity {
	public interface ILog4NetConfigurator : IUnityContainerExtensionConfigurator {
		/// <summary>
		/// Sets the name of the logger to retrieve
		/// </summary>
		ILog4NetConfigurator SetName(string name);
	}

	/// <summary>
	/// Configures the application to use log4net
	/// </summary>
	public class ConfigureLog4Net : UnityContainerExtension, ILog4NetConfigurator {
		private string loggerName;

		protected override void Initialize() {
			XmlConfigurator.Configure();
			Container.RegisterType<ILogger>(
				new ContainerControlledLifetimeManager(),
				new InjectionFactory(container => new Log4NetLogger(LogManager.GetLogger(loggerName)))
			);
		}

		public ILog4NetConfigurator SetName(string name) {
			loggerName = name;
			return this;
		}
	}
}