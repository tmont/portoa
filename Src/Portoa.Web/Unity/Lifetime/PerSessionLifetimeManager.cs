using System;
using System.Web;
using System.Web.SessionState;
using Microsoft.Practices.Unity;

namespace Portoa.Web.Unity.Lifetime {
	/// <summary>
	/// <see cref="LifetimeManager"/> whose lifetime lasts as long as the current
	/// HTTP session. The object is stored in <c>HttpContext.Current.Session</c>.
	/// </summary>
	public class PerSessionLifetimeManager : LifetimeManager {
		private readonly string key;

		/// <param name="key">The session key to store the value in, defaults to a guid</param>
		public PerSessionLifetimeManager(string key = null) {
			this.key = key ?? Guid.NewGuid().ToString();
		}

		private static HttpSessionState GetSession() {
			return HttpContext.Current != null && HttpContext.Current.Session != null 
				? HttpContext.Current.Session
				: null;
		}

		public override object GetValue() {
			var session = GetSession();
			return session != null ? session[key] : null;
		}

		public override void SetValue(object newValue) {
			var session = GetSession();
			if (session != null) {
				session[key] = newValue;
			}
		}

		public override void RemoveValue() {
			var session = GetSession();
			if (session != null) {
				session.Remove(key);
			}
		}
	}
}