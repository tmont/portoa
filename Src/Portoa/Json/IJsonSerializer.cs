namespace Portoa.Json {
	/// <summary>
	/// Object-to-JSON serializer
	/// </summary>
	public interface IJsonSerializer {
		/// <summary>
		/// Serializes an object to a JSON string
		/// </summary>
		/// <param name="o">The object to serialize</param>
		/// <returns>A JSON string representing <paramref name="o"/></returns>
		string Serialize(object o);

		/// <summary>
		/// Deserializes a JSON string into an object
		/// </summary>
		/// <typeparam name="T">The type of object to deserialize the JSON to</typeparam>
		/// <param name="json">The JSON string to deserialize</param>
		/// <returns>An object of type <typeparamref name="T"/> representing the given <paramref name="json"/></returns>
		T Deserialize<T>(string json);
	}
}