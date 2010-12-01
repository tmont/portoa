using System.Collections.Generic;
using System.Web.Mvc;

namespace Portoa.Web.Util {
	/// <summary> This class exists to get rid of the SessionState and TempData error. Just google it. </summary>
	public class NoTempDataProvider : ITempDataProvider {
		public IDictionary<string, object> LoadTempData(ControllerContext controllerContext) {
			return new Dictionary<string, object>();
		}

		/// <summary> Don't use this method, it's only here to prevent you from using it. </summary>
		public void SaveTempData(ControllerContext controllerContext, IDictionary<string, object> values) {

		}
	}
}