using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Portoa.Web.Models {
	/// <summary>
	/// Model binder for <c>enum</c> values
	/// </summary>
	/// <remarks>
	/// Adapted from http://eliasbland.wordpress.com/2009/08/08/enumeration-model-binder-for-asp-net-mvc/
	/// </remarks>
	public class EnumModelBinder<T> : IModelBinder {
		private readonly T defaultValue;
		private static readonly IDictionary<Type, string[]> enumNameCache = new Dictionary<Type, string[]>();
		private readonly Type enumType;

		public EnumModelBinder() : this(default(T)) { }

		public EnumModelBinder(T defaultValue) {
			this.defaultValue = defaultValue;
			enumType = typeof(T);
		}

		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
			var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
			return valueResult == null
				? defaultValue
				: GetEnumValue(valueResult.AttemptedValue);
		}

		private T GetEnumValue(string value) {
			return !string.IsNullOrEmpty(value) && Contains(value) ? (T)Enum.Parse(enumType, value, true) : defaultValue;
		}

		private bool Contains(string value) {
			if (!enumNameCache.ContainsKey(enumType)) {
				enumNameCache[enumType] = Enum.GetNames(enumType);
			}

			return enumNameCache[enumType].Contains(value, StringComparer.OrdinalIgnoreCase);
		}
	}
}