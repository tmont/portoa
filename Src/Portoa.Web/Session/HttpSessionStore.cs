using System.Web;

namespace Portoa.Web.Session {
	/// <summary>
	/// Session store that uses the current <c>HttpContext</c>
	/// </summary>
	public class HttpSessionStore : ISessionStore {
		public object this[string key] { 
			get { return HttpContext.Current.Session != null ? HttpContext.Current.Session[key] : null; }
			set { 
				if (HttpContext.Current.Session != null) {
					HttpContext.Current.Session[key] = value;
				}
			} 
		}
	}
}