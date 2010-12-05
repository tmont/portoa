using System;
using System.Web.Mvc;

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
		/// using the given service provider to resolve the binder
		/// </summary>
		public static ModelBinderDictionary Add<T, TBinder>(this ModelBinderDictionary binders, IServiceProvider serviceProvider) where TBinder : IModelBinder {
			binders.Add(typeof(T), new ServiceProviderModelBinder<TBinder>(serviceProvider));
			return binders;
		}

	}
}