using System;

namespace Portoa.Json {
	public static class JsonExtensions {
		/// <summary>
		/// Convenience method for deserializing json when the type cannot be known
		/// at compile-time
		/// </summary>
		/// <param name="json">The JSON string to deserialize into an object</param>
		/// <param name="type">The type of object that should be deserialized</param>
		public static object Deserialize(this IJsonSerializer serializer, string json, Type type = null) {
			return serializer
				.GetType()
				.GetMethod("Deserialize", new[] { typeof(string) })
				.MakeGenericMethod(type ?? typeof(object))
				.Invoke(serializer, new object[] { json });
		}
	}
}