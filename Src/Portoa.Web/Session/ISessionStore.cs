using JetBrains.Annotations;

namespace Portoa.Web.Session {
	/// <summary>
	/// Provides an interface for interacting with session data
	/// </summary>
	public interface ISessionStore {
		/// <summary>
		/// Gets or sets an item from the backing store
		/// </summary>
		/// <param name="key">The key of the item to retrieve</param>
		/// <returns>The value stored in the specified key, or null if no such key exists</returns>
		[CanBeNull]
		object this[string key] { get; set; }
	}
}