using System;
using System.ComponentModel;
using System.Configuration;

namespace Portoa.Web.Rest.Configuration {
	public class RestSectionHandler : ConfigurationSection {
		private Type criteriaHandlerType;

		[ConfigurationCollection(typeof(SpecialCriterionCollection), AddItemName = "criterion")]
		public SpecialCriterionCollection SpecialCriteria { get { return (SpecialCriterionCollection)this["specialCriterion"]; } }

		[ConfigurationProperty("idHandler", IsRequired = false), EditorBrowsable(EditorBrowsableState.Never)]
		public CriterionElement InternalGlobalIdHandler { get { return (CriterionElement)this["idHandler"]; } }

		[ConfigurationProperty("criteriaHandler", IsRequired = false), EditorBrowsable(EditorBrowsableState.Never)]
		public UnnamedTypedElement GlobalCriteriaHandler { get { return (UnnamedTypedElement)this["criteriaHandler"]; } }

		[ConfigurationCollection(typeof(ResourceCollection), AddItemName = "resource")]
		public ResourceCollection Resources { get { return (ResourceCollection)this["resources"]; } }

		/// <summary>
		/// Gets the global ID handler
		/// </summary>
		public CriterionElement GlobalIdHandler {
			get {
				return InternalGlobalIdHandler ?? Defaults.DefaultIdHandler;
			}
		}

		/// <summary>
		/// Gets the global criteria handler type
		/// </summary>
		public Type GlobalCriteriaHandlerType { 
			get {
				if (criteriaHandlerType != null) {
					return criteriaHandlerType;
				}

				if (GlobalCriteriaHandler != null) {
					return criteriaHandlerType = GlobalCriteriaHandler.Type;
				}

				return criteriaHandlerType = Defaults.DefaultCriteriaHandlerType;
			} 
		}
	}
}