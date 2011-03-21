using System;
using System.ComponentModel;
using System.Configuration;

namespace Portoa.Web.Rest.Configuration {
	public class UnnamedTypedElement : ConfigurationElement {
		private Type type;

		public UnnamedTypedElement() { }

		internal UnnamedTypedElement(Type type) {
			this.type = type;
		}

		public Type Type { get { return type ?? (type = Type.GetType(TypeName)); } }

		[ConfigurationProperty("type", IsRequired = true), EditorBrowsable(EditorBrowsableState.Never)]
		public string TypeName { get { return (string)this["type"]; } }
	}
}