using System.Configuration;
using JetBrains.Annotations;

namespace Portoa.Web.Rest.Configuration {
	public class SpecialCriterionCollection : ConfigurationElementCollection {
		protected override ConfigurationElement CreateNewElement() {
			return new CriterionElement();
		}

		protected override object GetElementKey(ConfigurationElement element) {
			return ((CriterionElement)element).Name;
		}

		[CanBeNull]
		public new CriterionElement this[string propertyName] {
			get {
				var element = BaseGet(propertyName);
				return element != null ? (CriterionElement)element : GetDefaultCriterion(propertyName);
			}
		}

		private static CriterionElement GetDefaultCriterion(string name) {
			switch (name) {
				case "sort":
					return Defaults.DefaultSortCriterion;
				case "limit":
					return Defaults.DefaultLimitCriterion;
				case "offset":
					return Defaults.DefaultOffsetCriterion;
				default:
					return null;
			}
		}
	}
}