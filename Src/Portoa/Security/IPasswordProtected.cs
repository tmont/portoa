namespace Portoa.Security {
	/// <summary>
	/// Represents an object that has a password associated with it
	/// </summary>
	public interface IPasswordProtected {
		/// <summary>
		/// Verifies the legitimacy of a potential password
		/// </summary>
		/// <param name="password">The password to verify</param>
		bool VerifyPassword(string password);

		/// <summary>
		/// Changes the object's password to the new password
		/// </summary>
		/// <param name="newPassword">The new password</param>
		void ChangePassword(string newPassword);
	}
}