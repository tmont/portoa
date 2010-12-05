using System;
using System.Web.Mvc;
using Portoa.Util;

namespace Portoa.Web.Models {
	/// <summary>
	/// Model binder that uses a service provider to resolve the model binder designated by
	/// <typeparamref name="T"/> which will do the action model binding
	/// </summary>
	/// <typeparam name="T">The model binder to resolve</typeparam>
	public class ResolveWithServiceProviderModelBinder<T> : IModelBinder where T : IModelBinder {
		private readonly IServiceProvider serviceProvider;

		public ResolveWithServiceProviderModelBinder(IServiceProvider serviceProvider) {
			this.serviceProvider = serviceProvider;
		}

		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
			return serviceProvider
				.GetService<T>()
				.BindModel(controllerContext, bindingContext);
		}
	}
}