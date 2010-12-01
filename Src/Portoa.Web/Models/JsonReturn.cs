using System.Collections.Generic;

namespace Portoa.Web.Models {
	public class JsonReturn {
		public string Error { get; set; }
		public IDictionary<string, string> Data { get; set; }
	}
}