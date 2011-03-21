using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Portoa.Web.Rest.Configuration {
	public class CriterionHandlerCollection : ConfigurationElementCollection, IEnumerable<CriterionHandlerElement> {
		protected override ConfigurationElement CreateNewElement() {
			return new CriterionHandlerElement();
		}

		protected override object GetElementKey(ConfigurationElement element) {
			return ((CriterionHandlerElement)element).Field;
		}

		[ConfigurationProperty("useDefaultForOmittedFields", IsRequired = false)]
		public bool UseDefaultForOmittedFields { get { return bool.Parse((string)this["useDefaultForOmittedFields"]); } }

		public new IEnumerator<CriterionHandlerElement> GetEnumerator() {
			return this.Cast<CriterionHandlerElement>().GetEnumerator();
		}
	}
}