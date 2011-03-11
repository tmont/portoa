using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Portoa.Util {
	public static class ReflectionExtensions {
		private const BindingFlags FetchFieldsFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

		/// <summary>
		/// Gets all attributes of the specified type for the given attribute provider
		/// </summary>
		/// <typeparam name="T">The type of attribute to get</typeparam>
		public static T[] GetAttributes<T>(this ICustomAttributeProvider provider) where T : Attribute {
			return (T[])provider.GetCustomAttributes(typeof(T), true);
		}

		/// <summary>
		/// Determines if the given <paramref cref="provider"/> has the specified attribute
		/// </summary>
		/// <typeparam name="T">The type of attribute to check for</typeparam>
		public static bool HasAttribute<T>(this ICustomAttributeProvider provider) where T : Attribute {
			return provider.GetAttributes<T>().Any();
		}

		/// <summary>
		/// Determines if the specified <paramref name="enum"/> has the given attribute
		/// </summary>
		/// <typeparam name="T">The type of attribute to check for</typeparam>
		public static bool HasAttribute<T>(this Enum @enum) where T : Attribute {
			return @enum.GetType().GetMember(@enum.ToString())[0].HasAttribute<T>();
		}

		/// <summary>
		/// Gets all attributes of the specified type for the given <paramref name="enum"/>
		/// </summary>
		/// <typeparam name="T">The type of attribute to get</typeparam>
		public static T[] GetAttributes<T>(this Enum @enum) where T : Attribute {
			return @enum.GetType().GetMember(@enum.ToString())[0].GetAttributes<T>();
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