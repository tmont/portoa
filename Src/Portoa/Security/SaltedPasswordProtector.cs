using System;
using System.Security.Cryptography;
using System.Text;

namespace Portoa.Security {
	public abstract class SaltedPasswordProtector : IPasswordProtected {
		public string Salt { get; set; }
		protected string Password { get; set; }

		public bool IsSet { get { return !string.IsNullOrEmpty(Password); } }

		protected SaltedPasswordProtector(string salt = null) {
			Salt = salt ?? MD5
			    .Create()
			    .ComputeHash(Encoding.UTF8.GetBytes(DateTime.Now.ToString())).ToHex();
		}

		public bool VerifyPassword(string potentialPassword) {
			if (string.IsNullOrEmpty(potentialPassword)) {
				return false;
			}

			return potentialPassword.ComputeHash(Salt).ToHex() == Password;
		}		
		
		public abstract void ChangePassword(string newPassword);
	}
}