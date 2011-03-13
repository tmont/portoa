using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Portoa.Util;

namespace Portoa.Validation {
	public static class ValidationExtensions {
		private const BindingFlags FetchFieldsFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

		/// <summary>
		/// Combines error messages into a single, readable error message
		/// </summary>
		/// <param name="separator">Separator to use to divide each error message</param>
		public static string CombineErrorMessages(this IValidationResultsProvider resultsProvider, string separator = "\n") {
			return resultsProvider.Results.Aggregate("Validation errors:", (current, next) => current + separator + next.ErrorMessage);
		}

		/// <summary>
		/// Gets all validatable fields from the given <paramref name="type"/>
		/// </summary>
		public static IEnumerable<FieldInfo> GetAllValidatableFields(this Type type) {
			return type
				.GetFields(FetchFieldsFlags)
				.WithoutBackingFields();
		}

		/// <summary>
		/// Gets all validatable properties from the given <paramref name="type"/>
		/// </summary>
		public static IEnumerable<PropertyInfo> GetAllValidatableProperties(this Type type) {
			return type
				.GetProperties(FetchFieldsFlags)
				.Where(property => property.GetIndexParameters().Length == 0 && !property.PropertyType.IsArray);
		}
	}
}