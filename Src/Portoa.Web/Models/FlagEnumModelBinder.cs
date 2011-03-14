using System;
using System.Linq;
using System.Web.Mvc;

namespace Portoa.Web.Models {
	/// <summary>
	/// Binds an enumeration annotated with <see cref="FlagsAttribute"/>, accounting for
	/// bitwise operations
	/// </summary>
	/// <remarks>
	/// Adapted from http://blog.nathan-taylor.net/2010/06/aspnet-mvc-flags-enumeration-model.html
	/// </remarks>
	public class FlagEnumModelBinder<T> : DefaultModelBinder {
		private readonly Type enumType;

		public FlagEnumModelBinder() {
			enumType = typeof(T);
			if (!enumType.IsEnum || !enumType.IsDefined(typeof(FlagsAttribute), false)) {
				throw new ArgumentException("The type param T must be an Enum annotated with FlagsAttribute");
			}
		}

		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
			var values = GetValuesFromProvider(bindingContext, bindingContext.ModelName);

			if (values.Length > 1) {
				var byteValue = values
					.Where(enumValue => Enum.IsDefined(enumType, enumValue))
					.Aggregate(0, (current, value) => current | (int)Enum.Parse(enumType, value));

				return Enum.Parse(enumType, byteValue.ToString());
			}

			return default(T);
		}

		private static string[] GetValuesFromProvider(ModelBindingContext bindingContext, string key) {
			if (bindingContext.ValueProvider.ContainsPrefix(key)) {
				var valueResult = bindingContext.ValueProvider.GetValue(key);
				bindingContext.ModelState.SetModelValue(key, valueResult);
				return (string[])valueResult.ConvertTo(typeof(string[]));
			}

			return new string[0];
		}
	}
}