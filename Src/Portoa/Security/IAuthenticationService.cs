namespace Portoa.Security {
	/// <summary>
	/// Service for authenticating users
	/// </summary>
	public interface IAuthenticationService : IAuthenticator {
		/// <summary>
		/// Logs in the specified user with the specified username
		/// </summary>
		void Login(string username);

		/// <summary>
		/// Logs out the currently logged in user
		/// </summary>
		void Logout();
	}
}