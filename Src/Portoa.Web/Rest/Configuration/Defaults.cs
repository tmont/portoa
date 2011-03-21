using System;

namespace Portoa.Web.Rest.Configuration {
	internal static class Defaults {
		public static readonly Type DefaultCriteriaHandlerType = typeof(DefaultCriteriaHandler);
		public static readonly CriterionElement DefaultIdHandler = new CriterionElement("id", "id", typeof(DefaultIdHandler));
		public static readonly CriterionElement DefaultSortCriterion = new CriterionElement("sort", ":sort", typeof(DefaultSortHandler));
		public static readonly CriterionElement DefaultLimitCriterion = new CriterionElement("limit", ":limit", typeof(DefaultLimitHandler));
		public static readonly CriterionElement DefaultOffsetCriterion = new CriterionElement("offset", ":offset", typeof(DefaultOffsetHandler));
	}
}