namespace Portoa.Css {
	/// <summary>
	/// Provides a mechanism for caching compiled CSS
	/// </summary>
	public interface ICssCacheStrategy {
		/// <summary>
		/// Gets the cached CSS at <c>key</c>, or <c>null</c>
		/// if the key isn't set
		/// </summary>
		/// <param name="key">The cache key</param>
		CssCacheItem Get(string key);

		/// <summary>
		/// Sets the cached CSS at <c>key</c>. This will override
		/// anything already set at the key.
		/// </summary>
		/// <param name="key">The cache key</param>
		/// <param name="compiled">Compiled CSS code</param>
		void Set(string key, string compiled, CssCacheOptions options = null);

		/// <summary>
		/// Deletes compiled CSS at <c>key</c>. If the key is not
		/// set, this is a no-op.
		/// </summary>
		/// <param name="key">The cache key</param>
		void Delete(string key);
	}
}