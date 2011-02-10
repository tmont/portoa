using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Portoa.Web.Unity {
	/// <summary>
	/// Reads the app config and applies the Unity configuration, if applicable
	/// </summary>
	public class ApplyUnityConfigurationSection : UnityContainerExtension {
		protected override void Initialize() {
			var section = ConfigurationManager.GetSection("unity") as UnityConfigurationSection;
			if (section == null) {
				return;
			}

			section.Configure(Container);
		}
	}
}
