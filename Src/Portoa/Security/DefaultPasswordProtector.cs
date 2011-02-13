using System;

namespace Portoa.Security {
	/// <summary>
	/// Default password protector that does not allow null or empty passwords
	/// </summary>
	public class DefaultPasswordProtector : SaltedPasswordProtector {
		public override void ChangePassword(string newPassword) {
			if (string.IsNullOrEmpty(newPassword)) {
				throw new ArgumentNullException("newPassword", "New password cannot be empty");
			}

			Password = newPassword.ComputeHash(Salt).ToHex();
		}
	}
}