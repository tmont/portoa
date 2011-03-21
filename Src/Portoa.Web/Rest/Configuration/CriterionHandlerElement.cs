using System;
using System.ComponentModel;
using System.Configuration;

namespace Portoa.Web.Rest.Configuration {
	public class CriterionHandlerElement : ConfigurationElement {
		private Type type;

		[ConfigurationProperty("field", IsRequired = true)]
		public string Field { get { return (string)this["field"]; } }

		[ConfigurationProperty("type", IsRequired = true), EditorBrowsable(EditorBrowsableState.Never)]
		public string TypeName { get { return (string)this["type"]; } }

		public Type Type { get { return type ?? (type = Type.GetType(TypeName)); } }
	}
}