using Newtonsoft.Json;

namespace Portoa.Json {
	public class JsonNetSerializer {
		public string Serialize(object o) {
			return JsonConvert.SerializeObject(o);
		}

		public T Deserialize<T>(string json) {
			return JsonConvert.DeserializeObject<T>(json);
		}
	}
}