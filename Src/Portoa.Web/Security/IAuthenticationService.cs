using Portoa.Logging;

namespace Portoa.Web.Security {
	/// <summary>
	/// Service for authenticating users
	/// </summary>
	public interface IAuthenticationService {
		/// <summary>
		/// Logs in the specified user with the specified username
		/// </summary>
		void Login(string username);

		/// <summary>
		/// Logs out the currently logged in user
		/// </summary>
		void Logout();

		/// <summary>
		/// Determines if a user identified by the given username and password
		/// is a valid user in the system
		/// </summary>
		bool IsValid(string username, [DoNotLog]string password);
	}
}