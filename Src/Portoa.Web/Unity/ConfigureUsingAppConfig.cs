using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Portoa.Web.Unity {
	/// <summary>
	/// Reads the app config and applies the Unity configuration, if applicable
	/// </summary>
	public class ConfigureUsingAppConfig : UnityContainerExtension {
		private readonly UnityConfigurationSection section;

		public ConfigureUsingAppConfig(UnityConfigurationSection section) {
			this.section = section;
		}

		protected override void Initialize() {
			if (section == null) {
				return;
			}

			Container.LoadConfiguration(section);
		}
	}
}
