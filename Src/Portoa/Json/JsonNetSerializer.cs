using Newtonsoft.Json;

namespace Portoa.Json {
	/// <summary>
	/// Object-to-JSON serializer backed by <c>JSON.NET</c>
	/// </summary>
	public class JsonNetSerializer : IJsonSerializer {
		public string Serialize(object o) {
			return JsonConvert.SerializeObject(o);
		}

		public T Deserialize<T>(string json) {
			return JsonConvert.DeserializeObject<T>(json);
		}
	}
}