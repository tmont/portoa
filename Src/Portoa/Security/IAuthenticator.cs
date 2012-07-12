namespace Portoa.Security {
	/// <summary>
	/// Object that verifies whether a user is valid
	/// </summary>
	public interface IAuthenticator {
		/// <summary>
		/// Determines whether a user with the given credentials is valid
		/// </summary>
		bool IsValid(AuthCredentials credentials);
	}
}