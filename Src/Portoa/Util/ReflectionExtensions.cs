using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Portoa.Util {
	public static class ReflectionExtensions {
		private const BindingFlags FetchFieldsFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

		public static T[] GetAttributes<T>(this ICustomAttributeProvider provider) where T : Attribute {
			return (T[])provider.GetCustomAttributes(typeof(T), true);
		}

		public static bool HasAttribute<T>(this ICustomAttributeProvider provider) where T : Attribute {
			return provider.GetAttributes<T>().Any();
		}

		public static bool HasAttribute<T>(this Enum e) where T : Attribute {
			return e.GetType().GetMember(e.ToString())[0].HasAttribute<T>();
		}

		public static IEnumerable<FieldInfo> GetAllValidatableFields(this Type type) {
			return type
				.GetFields(FetchFieldsFlags)
				.WithoutBackingFields();
		}

		public static IEnumerable<FieldInfo> WithoutBackingFields(this IEnumerable<FieldInfo> fields) {
			return fields.Where(fieldInfo => !fieldInfo.Name.StartsWith("<"));
		}

		public static IEnumerable<PropertyInfo> GetAllValidatableProperties(this Type type) {
			return type
				.GetProperties(FetchFieldsFlags)
				.Where(property => property.GetIndexParameters().Length == 0 && !property.PropertyType.IsArray);
		}

		/// <summary>
		/// Gets a human-readable string representing the given type
		/// </summary>
		/// <parparam name="fullyQualified">Whether to use the fully qualified name of each type</parparam>
		[DebuggerNonUserCode]
		public static string GetFriendlyName(this Type type, bool fullyQualified = true) {
			//the namespace is null if it's an anonymous type
			var name = (fullyQualified && type.Namespace != null) ? type.Namespace : string.Empty;

			if (type.IsGenericType) {
				//the substring crap gets rid of everything after the ` in Name, e.g. List`1 for List<T>
				name +=
					type.Name.Substring(0, type.Name.IndexOf("`"))
					+ "<"
					+ type
					  	.GetGenericArguments()
					  	.Select(genericType => genericType.GetFriendlyName(fullyQualified))
					  	.Aggregate((current, friendlyName) => current + ", " + friendlyName)
					+ ">";
			} else {
				if (type.Namespace == null) {
					name += "AnonymousType";
				} else {
					if (!string.IsNullOrEmpty(name)) {
						name += ".";
					}
					name += type.Name;
				}
			}

			return name;
		}
	}
}