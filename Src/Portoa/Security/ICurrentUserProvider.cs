using JetBrains.Annotations;

namespace Portoa.Security {
	/// <summary>
	/// Provides an interface for identifying the current user
	/// </summary>
	/// <typeparam name="T">The user type</typeparam>
	public interface ICurrentUserProvider<out T> where T : class {
		/// <summary>
		/// Returns the user currently performing actions on the site, or <c>null</c>
		/// if the user cannot be identified
		/// </summary>
		[CanBeNull]
		T CurrentUser { get; }
	}
}