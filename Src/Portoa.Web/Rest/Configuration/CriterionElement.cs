using System;
using System.ComponentModel;
using System.Configuration;

namespace Portoa.Web.Rest.Configuration {
	/// <summary>
	/// Represents a single criterion in the configuration file
	/// </summary>
	public class CriterionElement : ConfigurationElement {
		private string name;
		private string requestKey;
		private Type handlerType;

		public CriterionElement() { }

		internal CriterionElement(string name, string requestKey, Type handlerType) {
			this.name = name;
			this.requestKey = requestKey;
			this.handlerType = handlerType;
		}

		/// <summary>
		/// The type of the <see cref="ICriterionHandler"/> for this criterion
		/// </summary>
		public Type HandlerType { get { return handlerType ?? (handlerType = Type.GetType(HandlerTypeName)); } }

		[ConfigurationProperty("handler", IsRequired = true), EditorBrowsable(EditorBrowsableState.Never)]
		public string HandlerTypeName { get { return (string)this["handler"]; } }

		/// <summary>
		/// The key to look for in the value provider to retrieve the value from the request.
		/// This does not need to be unique, but it should be.
		/// </summary>
		[ConfigurationProperty("requestKey", IsRequired = true)]
		public string RequestKey { get { return requestKey ?? (requestKey = (string)this["requestKey"]); } }

		/// <summary>
		/// The unique name of the criterion. Reserved names are <c>sort</c>, <c>limit</c>,
		/// <c>id</c> and <c>offset</c>.
		/// </summary>
		[ConfigurationProperty("name", IsRequired = true, IsKey = true)]
		public string Name { get { return name ?? (name = (string)this["name"]); } }
	}
}