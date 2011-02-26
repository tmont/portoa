using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Portoa.Web.Util {
	public static class FilterInfoExtensions {
		/// <summary>
		/// Flattens a <c>FilterInfo</c> object into a single <c>IEnumerable</c> containing
		/// the <c>ActionFilter</c>, <c>ExceptionFilter</c>, <c>ResultFilter</c> and
		/// <c>AuthorizationFilter</c> collections
		/// </summary>
		public static IEnumerable<object> Flatten(this FilterInfo filterInfo) {
			return filterInfo
				.ResultFilters
				.Cast<object>()
				.Concat(filterInfo.ActionFilters)
				.Concat(filterInfo.ExceptionFilters)
				.Concat(filterInfo.AuthorizationFilters)
				.Distinct();
		}

	}
}