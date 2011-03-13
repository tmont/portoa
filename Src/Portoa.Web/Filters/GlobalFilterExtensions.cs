using System.Web.Mvc;

namespace Portoa.Web.Filters {
	public static class GlobalFilterExtensions {
		/// <summary>
		/// Adds a filter to the global filter collection
		/// </summary>
		/// <typeparam name="T">The type of filter to add</typeparam>
		public static GlobalFilterCollection Add<T>(this GlobalFilterCollection filters, int order = 0) where T : new() {
			filters.Add(new T(), order);
			return filters;
		}
	}
}