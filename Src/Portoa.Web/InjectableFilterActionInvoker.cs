using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Portoa.Util;

namespace Portoa.Web {
	/// <summary>
	/// Enables you to dynamically add filters to a ControllerActionInvoker. This class will also
	/// perform injection on all filters that are annotated with NeedsBuildUpAttribute.
	/// </summary>
	/// <remarks> Adapted from http://blog.ploeh.dk/2009/12/01/GlobalErrorHandlingInASPNETMVC.aspx </remarks>
	public class InjectableFilterActionInvoker : ControllerActionInvoker {
		private readonly IServiceProvider serviceProvider;
		private readonly IList<IExceptionFilter> exceptionFilters = new List<IExceptionFilter>();
		private readonly IList<IActionFilter> actionFilters = new List<IActionFilter>();
		private readonly IList<IResultFilter> resultFilters = new List<IResultFilter>();
		private readonly IList<IAuthorizationFilter> authorizationFilters = new List<IAuthorizationFilter>();

		public InjectableFilterActionInvoker(IServiceProvider serviceProvider) {
			this.serviceProvider = serviceProvider;
		}

		public InjectableFilterActionInvoker AddAuthorizationFilter<TFilter>() where TFilter : IAuthorizationFilter {
			authorizationFilters.Add(serviceProvider.GetService<TFilter>());
			return this;
		}

		public InjectableFilterActionInvoker AddAuthorizationFilter(IAuthorizationFilter filter) {
			authorizationFilters.Add(filter);
			return this;
		}

		public InjectableFilterActionInvoker AddExceptionFilter<TFilter>() where TFilter : IExceptionFilter {
			exceptionFilters.Add(serviceProvider.GetService<TFilter>());
			return this;
		}

		public InjectableFilterActionInvoker AddExceptionFilter(IExceptionFilter filter) {
			exceptionFilters.Add(filter);
			return this;
		}

		public InjectableFilterActionInvoker AddActionFilter<TFilter>() where TFilter : IActionFilter {
			actionFilters.Add(serviceProvider.GetService<TFilter>());
			return this;
		}

		public InjectableFilterActionInvoker AddActionFilter(IActionFilter filter)  {
			actionFilters.Add(filter);
			return this;
		}

		public InjectableFilterActionInvoker AddResultFilter<TFilter>() where TFilter : IResultFilter {
			resultFilters.Add(serviceProvider.GetService<TFilter>());
			return this;
		}

		public InjectableFilterActionInvoker AddResultFilter(IResultFilter filter) {
			resultFilters.Add(filter);
			return this;
		}

		/// <summary>
		/// Overridden to add the new filters to the default filters
		/// </summary>
		protected override FilterInfo GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor) {
			var filters = base.GetFilters(controllerContext, actionDescriptor);
			filters.ActionFilters.AddRange(actionFilters);
			filters.ExceptionFilters.AddRange(exceptionFilters);
			filters.ResultFilters.AddRange(resultFilters);
			filters.AuthorizationFilters.AddRange(authorizationFilters);

			var container = serviceProvider.GetService<IUnityContainer>();

			//perform injection if necessary
			filters
				.Flatten()
				.Where(filter => filter.GetType().HasAttribute<NeedsBuildUpAttribute>())
				.ToList()
				.ForEach(filter => container.BuildUp(filter.GetType(), filter, null));

			return filters;
		}
	}
}