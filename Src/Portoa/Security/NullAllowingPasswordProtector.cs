namespace Portoa.Security {
	/// <summary>
	/// Password protector that allows nulls
	/// </summary>
	public class NullAllowingPasswordProtector : SaltedPasswordProtector {
		public NullAllowingPasswordProtector() : this(null) { }
		public NullAllowingPasswordProtector(string salt) : base(salt) {}

		public override void ChangePassword(string newPassword) {
			Password = string.IsNullOrEmpty(newPassword) ? null : newPassword.ComputeHash(Salt).ToHex();
		}
	}
}