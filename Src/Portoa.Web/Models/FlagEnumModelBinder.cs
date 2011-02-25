using System;
using System.Linq;
using System.Web.Mvc;

namespace Portoa.Web.Models {
	/// <summary>
	/// Binds an enumeration annotated with the <c>FlagsAttribute</c>
	/// </summary>
	/// <remarks>
	/// Adapted from http://blog.nathan-taylor.net/2010/06/aspnet-mvc-flags-enumeration-model.html
	/// </remarks>
	public class FlagEnumModelBinder<T> : DefaultModelBinder {
		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
			if (bindingContext == null) {
				throw new ArgumentNullException("bindingContext");
			}

			var values = GetValuesFromProvider(bindingContext, bindingContext.ModelName);

			if (values.Length > 1 && (bindingContext.ModelType.IsEnum && bindingContext.ModelType.IsDefined(typeof(FlagsAttribute), false))) {
				long byteValue = 0;
				foreach (var value in values.Where(enumValue => Enum.IsDefined(bindingContext.ModelType, enumValue))) {
					byteValue |= (int)Enum.Parse(bindingContext.ModelType, value);
				}

				return Enum.Parse(bindingContext.ModelType, byteValue.ToString());
			}

			return default(T);
		}

		private static string[] GetValuesFromProvider(ModelBindingContext bindingContext, string key) {
			if (bindingContext.ValueProvider.ContainsPrefix(key)) {
				var valueResult = bindingContext.ValueProvider.GetValue(key);
				if (valueResult != null) {
					bindingContext.ModelState.SetModelValue(key, valueResult);
					return (string[])valueResult.ConvertTo(typeof(string[]));
				}
			}

			return new string[0];
		}
	}

}