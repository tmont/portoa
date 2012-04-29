using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using JetBrains.Annotations;

namespace Portoa.Web.Filters {
	/// <summary>
	/// <see cref="IFilterProvider"/> that allows some adjustment to take place when
	/// collecting the filters, such as running injection on each filter before applying
	/// them to the controller action
	/// </summary>
	public class AdjustableFilterProvider : IFilterProvider, IEnumerable<IFilterProvider> {
		private readonly IFilterAdjuster filterAdjuster;
		private readonly IList<IFilterProvider> providers = new List<IFilterProvider>();

		/// <param name="filterAdjuster">Function to run on each filter, which should return the adjusted filter</param>
		/// <param name="initWithDefaultMvcProviders"><c>false</c> if using the default ASP.NET MVC filter providers is undesired</param>
		public AdjustableFilterProvider([NotNull]IFilterAdjuster filterAdjuster, bool initWithDefaultMvcProviders = true) {
			this.filterAdjuster = filterAdjuster;

			if (initWithDefaultMvcProviders) {
				providers.Add(GlobalFilters.Filters);
				providers.Add(new FilterAttributeFilterProvider());
				providers.Add(new ControllerInstanceFilterProvider());
			}
		}

		/// <summary>
		/// Adds a filter provider to the collection
		/// </summary>
		/// <param name="provider">The filter provider to add</param>
		public void Add([NotNull]IFilterProvider provider) {
			providers.Add(provider);
		}

		public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor) {
			return providers.SelectMany(provider => provider.GetFilters(controllerContext, actionDescriptor).Select(filter => filterAdjuster.AdjustFilter(filter)));
		}

		public IEnumerator<IFilterProvider> GetEnumerator() {
			return providers.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}