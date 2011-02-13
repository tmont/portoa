﻿using System.IO;
using log4net;
using log4net.Config;
using Microsoft.Practices.Unity;
using Portoa.Log4Net;
using Portoa.Logging;

namespace Portoa.Web.Unity {
	/// <summary>
	/// Exposes an interface to configure log4net
	/// </summary>
	public interface ILog4NetConfigurator : IUnityContainerExtensionConfigurator {
		/// <summary>
		/// Sets the name of the logger to retrieve
		/// </summary>
		/// <param name="loggerName">Name of the logger to use</param>
		ILog4NetConfigurator SetName(string loggerName);

		/// <summary>
		/// Uses the <c>XmlConfigurator</c> to configure log4net
		/// </summary>
		/// <param name="filename">Optional filename to watch, otherwise it uses the default</param>
		ILog4NetConfigurator UseXml(string filename = null);
	}

	/// <summary>
	/// Configures the application to use log4net
	/// </summary>
	public class ConfigureLog4Net : UnityContainerExtension, ILog4NetConfigurator {
		private string name;

		protected override void Initialize() {
			Container.RegisterType<ILogger>(
				new ContainerControlledLifetimeManager(),
				new InjectionFactory(container => new Log4NetLogger(LogManager.GetLogger(name)))
			);
		}

		public ILog4NetConfigurator UseXml(string filename = null) {
			if (!string.IsNullOrEmpty(filename)) {
				XmlConfigurator.ConfigureAndWatch(new FileInfo(filename));
			} else {
				XmlConfigurator.Configure();
			}

			return this;
		}

		public ILog4NetConfigurator SetName(string loggerName) {
			name = loggerName;
			return this;
		}
	}
}