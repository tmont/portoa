using System;
using System.Web;
using Microsoft.Practices.Unity;

namespace Portoa.Web.Unity.Lifetime {
	/// <summary>
	/// <see cref="LifetimeManager"/> whose lifetime lasts as long as the HTTP request.
	/// The object is stored in <c>HttpContext.Current.Items</c>, keyed by a <see cref="Guid"/>.
	/// </summary>
	public class PerRequestLifetimeManager : LifetimeManager {
		private readonly Guid key = Guid.NewGuid();

		public override object GetValue() {
			return HttpContext.Current == null ? null : HttpContext.Current.Items[key];
		}

		public override void RemoveValue() {
			if (HttpContext.Current != null) {
				HttpContext.Current.Items.Remove(key);
			}
		}

		public override void SetValue(object newValue) {
			if (HttpContext.Current != null) {
				HttpContext.Current.Items.Add(key, newValue);
			}
		}
	}
}