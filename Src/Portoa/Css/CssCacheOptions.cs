using System;

namespace Portoa.Css {
	/// <summary>
	/// Various options for setting a cache value
	/// </summary>
	public class CssCacheOptions {
		public CssCacheOptions() {
			Ttl = TimeSpan.MaxValue;
		}

		/// <summary>
		/// The time-to-live for the cached value. Default is forever.
		/// </summary>
		public TimeSpan Ttl { get; set; }
	}
}