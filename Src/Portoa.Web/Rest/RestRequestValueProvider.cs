using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;

namespace Portoa.Web.Rest {
	/// <summary>
	/// Enumerable value provider for REST requests
	/// </summary>
	public class RestRequestValueProvider : IEnumerable<KeyValuePair<string, ValueProviderResult>>, IValueProvider {
		private readonly IDictionary<string, ValueProviderResult> values = new Dictionary<string, ValueProviderResult>();

		public RestRequestValueProvider(ControllerContext controllerContext) {
			//query string
			foreach (var key in controllerContext.HttpContext.Request.QueryString.AllKeys) {
				values[key] = CreateResult(controllerContext.HttpContext.Request.QueryString[key]);
			}

			//form
			foreach (var key in controllerContext.HttpContext.Request.Form.AllKeys) {
				values[key] = CreateResult(controllerContext.HttpContext.Request.Form[key]);
			}

			//route data
			foreach (var routeValue in controllerContext.RouteData.Values) {
				values[routeValue.Key] = CreateResult(routeValue.Value);
			}

			//json?
		}

		private static ValueProviderResult CreateResult(object value) {
			return new ValueProviderResult(value, Convert.ToString(value, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
		}

		public bool ContainsPrefix(string prefix) {
			return values.ContainsKey(prefix);
		}

		public ValueProviderResult GetValue(string key) {
			return values[key];
		}

		public IEnumerator<KeyValuePair<string, ValueProviderResult>> GetEnumerator() {
			return values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}