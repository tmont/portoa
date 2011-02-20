using System;
using System.Web.Mvc;
using Portoa.Util;

namespace Portoa.Web.Models {
	/// <summary>
	/// Model binder that uses a service provider to resolve the model binder designated by
	/// <typeparamref name="T"/> which will do the actual model binding
	/// </summary>
	/// <typeparam name="T">The model binder to resolve</typeparam>
	/// <seealso cref="ResolveWithContainerModelBinder{T}"/>
	public class ResolveWithServiceProviderModelBinder<T> : IModelBinder where T : IModelBinder {
		private readonly IServiceProvider serviceProvider;

		/// <param name="serviceProvider">The service provider to use to resolve the model binder</param>
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