using System;
using System.Configuration;
using JetBrains.Annotations;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Portoa.Web.Unity {

	/// <summary>
	/// Exposes an interface to apply configuration to an extension that configures
	/// Unity using information contained in a config file
	/// </summary>
	public interface IUnityConfigurator : IUnityContainerExtensionConfigurator {
		/// <summary>
		/// Sets the name of the Unity configuration section in the config file
		/// </summary>
		/// <param name="name">The section name</param>
		IUnityConfigurator SetSectionName([NotNull]string name);
	}

	/// <summary>
	/// Reads the app config and applies the Unity configuration, if applicable
	/// </summary>
	public class ApplyUnityConfigurationSection : UnityContainerExtension, IUnityConfigurator {
		public const string DefaultSectionName = "unity";
		private string sectionName;

		protected override void Initialize() {
			var section = ConfigurationManager.GetSection(sectionName ?? DefaultSectionName) as UnityConfigurationSection;
			if (section == null) {
				return;
			}

			Container.LoadConfiguration(section);
		}

		public IUnityConfigurator SetSectionName(string name) {
			sectionName = name;
			return this;
		}
	}
}
