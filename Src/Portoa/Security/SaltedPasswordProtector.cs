using System;
using System.Security.Cryptography;
using System.Text;

namespace Portoa.Security {
	/// <summary>
	/// Password protector that hashes the password using a salt, and stores it
	/// as a hex-encoded string
	/// </summary>
	public abstract class SaltedPasswordProtector : IPasswordProtected {
		/// <summary>
		/// The salt used to hash the password as a hex-encoded string
		/// </summary>
		public string Salt { get; set; }

		/// <summary>
		/// The password as a hex-encoded string
		/// </summary>
		protected string Password { get; set; }

		/// <summary>
		/// Gets whether or not the password has been set
		/// </summary>
		public bool IsSet { get { return !string.IsNullOrEmpty(Password); } }

		protected SaltedPasswordProtector(string salt = null) {
			Salt = salt ?? MD5
			    .Create()
			    .ComputeHash(Encoding.UTF8.GetBytes(DateTime.Now.ToString())).ToHex();
		}

		/// <summary>
		/// Verifies the given password. 
		/// </summary>
		/// <returns>Returns false if <paramref name="potentialPassword"/> is null or empty</returns>
		public bool VerifyPassword(string potentialPassword) {
			if (string.IsNullOrEmpty(potentialPassword)) {
				return false;
			}

			return potentialPassword.ComputeHash(Salt).ToHex() == Password;
		}		
		
		public abstract void ChangePassword(string newPassword);
	}
}