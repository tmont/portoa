using System;

namespace Portoa.Security {
	public class DefaultPasswordProtector : SaltedPasswordProtector {
		public override void ChangePassword(string newPassword) {
			if (string.IsNullOrEmpty(newPassword)) {
				throw new ArgumentNullException("newPassword", "New password cannot be empty");
			}

			Password = newPassword.ComputeHash(Salt).ToHex();
		}
	}
}