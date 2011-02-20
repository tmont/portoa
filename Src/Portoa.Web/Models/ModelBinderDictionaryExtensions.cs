using System;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace Portoa.Web.Models {
	public static class ModelBinderDictionaryExtensions {
		/// <summary>
		/// Maps the type <typeparamref name="T"/> to the binder type <typeparamref name="TBinder"/>.
		/// </summary>
		public static ModelBinderDictionary Add<T, TBinder>(this ModelBinderDictionary binders) where TBinder : IModelBinder, new() {
			binders.Add(typeof(T), new TBinder());
			return binders;
		}

		/// <summary>
		/// Maps the type <typeparamref name="T"/> to the binder type <typeparamref name="TBinder"/>
		/// using the <paramref name="serviceProvider"/> to resolve the binder
		/// </summary>
		/// <see cref="ResolveWithServiceProviderModelBinder{T}"/>
		public static ModelBinderDictionary Add<T, TBinder>(this ModelBinderDictionary binders, IServiceProvider serviceProvider) where TBinder : IModelBinder {
			binders.Add(typeof(T), new ResolveWithServiceProviderModelBinder<TBinder>(serviceProvider));
			return binders;
		}

		/// <summary>
		/// Maps the type <typeparamref name="T"/> to the binder type <typeparamref name="TBinder"/>
		/// using the <paramref name="container"/> to resolve the binder
		/// </summary>
		/// <see cref="ResolveWithContainerModelBinder{T}"/>
		public static ModelBinderDictionary Add<T, TBinder>(this ModelBinderDictionary binders, IUnityContainer container) where TBinder : IModelBinder {
			binders.Add(typeof(T), new ResolveWithContainerModelBinder<TBinder>(container));
			return binders;
		}
	}
}