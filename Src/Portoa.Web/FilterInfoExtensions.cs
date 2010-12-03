using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Portoa.Web {
	public static class FilterInfoExtensions {
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