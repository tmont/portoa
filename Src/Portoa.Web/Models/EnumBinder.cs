using System;
using System.Linq;
using System.Web.Mvc;

namespace Portoa.Web.Models {
	/// <remarks>
	/// Adapted from http://eliasbland.wordpress.com/2009/08/08/enumeration-model-binder-for-asp-net-mvc/
	/// </remarks>
	public class EnumBinder<T> : IModelBinder {
		private readonly T defaultValue;

		public EnumBinder() : this(default(T)) { }

		public EnumBinder(T defaultValue) {
			this.defaultValue = defaultValue;
		}

		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
			return bindingContext.ValueProvider.GetValue(bindingContext.ModelName) == null
			       	? defaultValue
			       	: GetEnumValue(bindingContext.ValueProvider.GetValue(bindingContext.ModelName).AttemptedValue);
		}

		private T GetEnumValue(string value) {
			return !string.IsNullOrEmpty(value) && Contains(value) ? (T)Enum.Parse(typeof(T), value, true) : defaultValue;
		}

		private static bool Contains(string value) {
			return Enum.GetNames(typeof(T)).Contains(value, StringComparer.OrdinalIgnoreCase);
		}
	}
}