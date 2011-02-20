using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace Portoa.Web.Models {
	/// <summary>
	/// Model binder that uses an <c>IUnityContainer</c> to resolve the model binder designated by
	/// <typeparamref name="T"/> which will do the actual model binding
	/// </summary>
	/// <typeparam name="T">The model binder to resolve</typeparam>
	/// <seealso cref="ResolveWithServiceProviderModelBinder{T}"/>
	public class ResolveWithContainerModelBinder<T> : IModelBinder where T : IModelBinder {
		private readonly IUnityContainer container;

		/// <param name="container">The container to use to resolve the model binder</param>
		public ResolveWithContainerModelBinder(IUnityContainer container) {
			this.container = container;
		}

		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
			return container
				.Resolve<T>()
				.BindModel(controllerContext, bindingContext);
		}
	}
}