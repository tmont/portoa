using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Portoa.Util;
using Portoa.Web.Filters;

namespace Portoa.Web.Unity {
	/// <summary>
	/// Performs injection on each filter that contains a <see cref="NeedsBuildUpAttribute"/>
	/// </summary>
	public class InjectionFilterAdjuster : IFilterAdjuster {
		private readonly IUnityContainer container;

		public InjectionFilterAdjuster(IUnityContainer container) {
			this.container = container;
		}

		public Filter AdjustFilter(Filter filter) {
			var instanceType = filter.Instance.GetType();
			return instanceType.HasAttribute<NeedsBuildUpAttribute>()
				? new Filter(container.BuildUp(instanceType, filter.Instance), filter.Scope, filter.Order)
				: filter;
		}
	}
}