using System;
using System.ComponentModel;
using System.Configuration;
using Portoa.Web.Rest.Parser;

namespace Portoa.Web.Rest.Configuration {
	public class ResourceElement : ConfigurationElement {
		public static readonly Type DefaultCriterionParserFactoryType = typeof(DefaultCriterionParserFactory);
		public static readonly Type DefaultIdParserType = typeof(IdentityIdParser);
		public static readonly Type DefaultCriteriaHandlerType = typeof(DefaultCriteriaHandler);

		private Type type;
		private Type criterionParserFactoryType;
		private Type idParserType;
		private Type criteriaHandlerType;

		[ConfigurationProperty("key", IsRequired = true)]
		public string Key { get { return (string)this["key"]; } }

		[ConfigurationProperty("type", IsRequired = true), EditorBrowsable(EditorBrowsableState.Never)]
		public string TypeName { get { return (string)this["type"]; } }

		[ConfigurationProperty("idParser", IsRequired = false), EditorBrowsable(EditorBrowsableState.Never)]
		public UnnamedTypedElement IdParser { get { return (UnnamedTypedElement)this["idParser"]; } }

		[ConfigurationProperty("criterionParserFactory", IsRequired = false), EditorBrowsable(EditorBrowsableState.Never)]
		public UnnamedTypedElement CriterionParserFactory { get { return (UnnamedTypedElement)this["criterionParserFactory"]; } }

		[ConfigurationProperty("criteriaHandler", IsRequired = false), EditorBrowsable(EditorBrowsableState.Never)]
		public UnnamedTypedElement CriteriaHandler { get { return (UnnamedTypedElement)this["criteriaHandler"]; } }

		[ConfigurationCollection(typeof(CriterionHandlerCollection), AddItemName = "add")]
		public CriterionHandlerCollection CriterionHandlers { get { return (CriterionHandlerCollection)this["criterionHandlers"]; } }

		public Type Type { get { return type ?? (type = Type.GetType(TypeName)); } }
		public Type CriterionParserFactoryType { get { return criterionParserFactoryType ?? (criterionParserFactoryType = CriterionParserFactory.Type ?? DefaultCriterionParserFactoryType); } }
		public Type IdParserType { get { return idParserType ?? (idParserType = IdParser.Type ?? DefaultIdParserType); } }
		public Type CriteriaHandlerType { get { return criteriaHandlerType ?? (criteriaHandlerType = CriteriaHandler.Type ?? DefaultCriteriaHandlerType); } }
	}
}