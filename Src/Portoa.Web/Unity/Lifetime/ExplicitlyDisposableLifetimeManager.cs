using System;
using JetBrains.Annotations;
using Microsoft.Practices.Unity;

namespace Portoa.Web.Unity.Lifetime {
	/// <summary>
	/// <see cref="LifetimeManager"/> decorator that allows you to explicitly control
	/// how the object disposes (useful for 3rd-party objects that do not implement 
	/// <see cref="IDisposable"/>)
	/// </summary>
	public class ExplicitlyDisposableLifetimeManager<T> : LifetimeManager, IDisposable {
		private readonly LifetimeManager lifetimeManager;
		private readonly Action<T> dispose;

		/// <param name="lifetimeManager">The <see cref="LifetimeManager"/> to decorate. This should be a <see cref="LifetimeManager"/>
		/// that actually has a backing store, e.g. <see cref="TransientLifetimeManager"/> will not be properly 
		/// disposed because there is no backing store for the objects it manages.</param>
		/// <param name="dispose">Action to call when the value needs to be disposed</param>
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
			lifetimeManager.RemoveValue();
		}

		public void Dispose() {
			var value = GetValue();
			if (value is T) {
				dispose((T)value);
			}
		}
	}
}