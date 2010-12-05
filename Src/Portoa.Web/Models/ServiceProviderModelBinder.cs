using System;
using System.Web.Mvc;
using Portoa.Util;

namespace Portoa.Web.Models {
	public class ServiceProviderModelBinder<T> : IModelBinder where T : IModelBinder {
		private readonly IServiceProvider serviceProvider;

		public ServiceProviderModelBinder(IServiceProvider serviceProvider) {
			this.serviceProvider = serviceProvider;
		}

		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
			return serviceProvider
				.GetService<T>()
				.BindModel(controllerContext, bindingContext);
		}
	}
}