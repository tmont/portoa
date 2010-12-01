using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Portoa.Util;

namespace Portoa.EventLog {
	public static class EventLogExtensions {

		const BindingFlags FetchPropertiesFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;

		public static void Register(this IEventLoggable entity, IEventLogPropertyHandlerFactory propertyFactory) {
			var type = entity.GetType();

			//<3 linq
			var originalData = type
				.GetProperties(FetchPropertiesFlags)
				.Concat(type.GetFields(FetchPropertiesFlags).WithoutBackingFields().Cast<MemberInfo>())
				.Where(memberInfo => memberInfo.DeclaringType != typeof(IEventLoggable) && memberInfo.HasAttribute<EventLogPropertyAttribute>())
				.ToDictionary<MemberInfo, MemberInfo, object>(
					memberInfo => memberInfo,
					memberInfo => propertyFactory.GetPropertyValue(memberInfo, entity)
				);

			type.GetProperty("OriginalData").SetValue(entity, originalData, null);
		}

		public static IDictionary<MemberInfo, EventData> GetEventData(this IEventLoggable entity, IEventLogPropertyHandlerFactory propertyFactory) {
			return entity
				.OriginalData
				.ToDictionary(kvp => kvp.Key, kvp => new EventData {
					Original = kvp.Value,
					Current = propertyFactory.GetPropertyValue(kvp.Key, entity)
				});
		}

		public static string FormatEventData(this IEventLoggable loggableEvent, IEventLogPropertyHandlerFactory propertyFactory, string separator = "\n") {
			return loggableEvent
				.GetEventData(propertyFactory)
				.Where(kvp => kvp.Value.Current != kvp.Value.Original)
				.Aggregate(new StringBuilder(), (current, kvp) => current.Append(separator + kvp.Key.Name + ": " + kvp.Value.Original + " => " + kvp.Value.Current))
				.ToString()
				.TrimStart(separator.ToArray());
		}

		internal static Type GetPropertyOrFieldType(this MemberInfo memberInfo) {
			return memberInfo is PropertyInfo ? ((PropertyInfo)memberInfo).PropertyType : ((FieldInfo)memberInfo).FieldType;
		}

		internal static object GetPropertyOrFieldValue(this MemberInfo memberInfo, object obj) {
			return memberInfo is PropertyInfo ? ((PropertyInfo)memberInfo).GetValue(obj, null) : ((FieldInfo)memberInfo).GetValue(obj);
		}

		internal static object GetPropertyValue(this IEventLogPropertyHandlerFactory factory, MemberInfo memberInfo, object entity) {
			return factory
				.Create(memberInfo.GetAttributes<EventLogPropertyAttribute>().Single().ItemHandler)
				.HandleProperty(memberInfo.GetPropertyOrFieldValue(entity));
		}

	}
}