using System.Web.Mvc;

namespace Portoa.Web.Filters {
	/// <summary>
	/// Provides a mechanism with which to modify filters as they go through
	/// the MVC pipeline
	/// </summary>
	public interface IFilterAdjuster {
		Filter AdjustFilter(Filter filter);
	}
}