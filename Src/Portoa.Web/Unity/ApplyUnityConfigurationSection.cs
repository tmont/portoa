using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Portoa.Web.Unity {
	public class ApplyUnityConfigurationSection : UnityContainerExtension {
		protected override void Initialize() {
			((UnityConfigurationSection)ConfigurationManager.GetSection("unity")).Configure(Container);
		}
	}
}
