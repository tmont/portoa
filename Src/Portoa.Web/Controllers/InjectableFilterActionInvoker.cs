﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Portoa.Util;
using Portoa.Web.Unity;
using Portoa.Web.Util;

namespace Portoa.Web.Controllers {
	/// <summary>
	/// Enables you to dynamically add filters to a ControllerActionInvoker. This class will also
	/// perform injection on all filters that are annotated with NeedsBuildUpAttribute.
	/// </summary>
	/// <remarks> Adapted from http://blog.ploeh.dk/2009/12/01/GlobalErrorHandlingInASPNETMVC.aspx </remarks>
	[Obsolete("Upgrade to ASP.NET MVC 3 and use GlobalFilters")]
	public class InjectableFilterActionInvoker : ControllerActionInvoker {
		private readonly IUnityContainer container;
		private readonly IList<IExceptionFilter> exceptionFilters = new List<IExceptionFilter>();
		private readonly IList<IActionFilter> actionFilters = new List<IActionFilter>();
		private readonly IList<IResultFilter> resultFilters = new List<IResultFilter>();
		private readonly IList<IAuthorizationFilter> authorizationFilters = new List<IAuthorizationFilter>();

		public InjectableFilterActionInvoker(IUnityContainer container) {
			this.container = container;
		}

		#region adding stuff
		public InjectableFilterActionInvoker AddAuthorizationFilter<TFilter>() where TFilter : IAuthorizationFilter {
			authorizationFilters.Add(container.Resolve<TFilter>());
			return this;
		}

		public InjectableFilterActionInvoker AddAuthorizationFilter(IAuthorizationFilter filter) {
			authorizationFilters.Add(filter);
			return this;
		}

		public InjectableFilterActionInvoker AddExceptionFilter<TFilter>() where TFilter : IExceptionFilter {
			exceptionFilters.Add(container.Resolve<TFilter>());
			return this;
		}

		public InjectableFilterActionInvoker AddExceptionFilter(IExceptionFilter filter) {
			exceptionFilters.Add(filter);
			return this;
		}

		public InjectableFilterActionInvoker AddActionFilter<TFilter>() where TFilter : IActionFilter {
			actionFilters.Add(container.Resolve<TFilter>());
			return this;
		}

		public InjectableFilterActionInvoker AddActionFilter(IActionFilter filter)  {
			actionFilters.Add(filter);
			return this;
		}

		public InjectableFilterActionInvoker AddResultFilter<TFilter>() where TFilter : IResultFilter {
			resultFilters.Add(container.Resolve<TFilter>());
			return this;
		}

		public InjectableFilterActionInvoker AddResultFilter(IResultFilter filter) {
			resultFilters.Add(filter);
			return this;
		}
		#endregion

		/// <summary>
		/// Overridden to add the new filters to the default filters
		/// </summary>
		protected override FilterInfo GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor) {
			var filters = base.GetFilters(controllerContext, actionDescriptor);
			filters.ActionFilters.AddRange(actionFilters);
			filters.ExceptionFilters.AddRange(exceptionFilters);
			filters.ResultFilters.AddRange(resultFilters);
			filters.AuthorizationFilters.AddRange(authorizationFilters);

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