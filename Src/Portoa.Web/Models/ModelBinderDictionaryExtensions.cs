using System.Web.Mvc;

namespace Portoa.Web.Models {
	public static class ModelBinderDictionaryExtensions {
		public static ModelBinderDictionary Add<T, TBinder>(this ModelBinderDictionary binders) where TBinder : IModelBinder, new() {
			binders.Add(typeof(T), new TBinder());
			return binders;
		}
	}
}