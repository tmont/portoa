using System;
using JetBrains.Annotations;
using Microsoft.Practices.Unity;

namespace Portoa.Web.Unity.Lifetime {
	/// <summary>
	/// <see cref="LifetimeManager"/> decorator that allows you to explicitly control
	/// how the object disposes (useful for 3rd-party objects that do not implement 
	/// <see cref="IDisposable"/>)
	/// </summary>
	public class ExplicitlyDisposableLifetimeManager<T> : LifetimeManager {
		private readonly LifetimeManager lifetimeManager;
		private readonly Action<T> dispose;

		/// <param name="lifetimeManager">The <c>LifetimeManager</c> to decorate</param>
		/// <param name="dispose">Action to call when the value is removed to dispose of the object</param>
		public ExplicitlyDisposableLifetimeManager([NotNull]LifetimeManager lifetimeManager, [NotNull]Action<T> dispose) {
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
			dispose((T)GetValue());
			lifetimeManager.RemoveValue();
		}
	}
}