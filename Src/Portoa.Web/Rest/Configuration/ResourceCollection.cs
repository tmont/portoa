using System.Linq;
using System.Collections.Generic;
using System.Configuration;

namespace Portoa.Web.Rest.Configuration {
	public class ResourceCollection : ConfigurationElementCollection, IEnumerable<ResourceElement> {
		protected override ConfigurationElement CreateNewElement() {
			return new ResourceElement();
		}

		protected override object GetElementKey(ConfigurationElement element) {
			return ((ResourceElement)element).Key;
		}

		public new IEnumerator<ResourceElement> GetEnumerator() {
			return this.Cast<ResourceElement>().GetEnumerator();
		}
	}
}