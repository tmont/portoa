using System;

namespace Portoa.Css {
	/// <summary>
	/// Represents an item in the CSS cache
	/// </summary>
	[Serializable]
	public class CssCacheItem {
		/// <summary>
		/// The time at which the cache item was created
		/// </summary>
		public DateTime Created { get; set; }

		/// <summary>
		/// The lifetime of the cache item
		/// </summary>
		public TimeSpan Ttl { get; set; }

		/// <summary>
		/// The CSS code stored in the cache
		/// </summary>
		public string Css { get; set; }

		public override string ToString() {
			return Css ?? string.Empty;
		}
	}
}