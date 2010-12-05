using System;
using System.Web;

namespace Portoa.Web {
	public static class HttpRequestBaseExtensions {
		public static T Get<T>(this HttpRequestBase request, string key) {
			var value = request[key];
			if (value == null) {
				return default(T);
			}

			try {
				return (T)Convert.ChangeType(value, typeof(T));
			} catch {
				return default(T);
			}
		}
	}
}