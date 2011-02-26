using System;
using System.Web;

namespace Portoa.Web.Util {
	public static class HttpRequestBaseExtensions {
		/// <summary>
		/// Gets an object from the request variables, or its default value if
		/// the key does not exist
		/// </summary>
		/// <typeparam name="T">The type to convert the value to</typeparam>
		/// <param name="key">The request key of the object to retrieve</param>
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