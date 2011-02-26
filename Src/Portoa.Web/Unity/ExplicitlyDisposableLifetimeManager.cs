using System;
using Microsoft.Practices.Unity;

namespace Portoa.Web.Unity {
	/// <summary>
	/// <see cref="LifetimeManager"/> decorator that allows you to explicitly control
	/// how the object disposes (useful for 3rd-party objects that do not implement 
	/// <see cref="IDisposable"/>)
	/// </summary>
	public class ExplicitlyDisposableLifetimeManager : LifetimeManager {
		private readonly LifetimeManager lifetimeManager;
		private readonly Action<object> dispose;

		/// <param name="lifetimeManager">The <c>LifetimeManager</c> to decorate</param>
		/// <param name="dispose">Action to call when the value is removed to dispose of the object</param>
		public ExplicitlyDisposableLifetimeManager(LifetimeManager lifetimeManager, Action<object> dispose) {
			this.lifetimeManager = lifetimeManager;
			this.dispose = dispose;
		}

		public override object GetValue() {
			return lifetimeManager.GetValue();
		}

		public override void SetValue(object newValue) {
			lifetimeManager.SetValue(newValue);
		}

		public override void RemoveValue() {
			dispose(GetValue());
			lifetimeManager.RemoveValue();
		}
	}
}