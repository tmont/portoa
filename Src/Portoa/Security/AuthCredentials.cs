namespace Portoa.Security {
	/// <summary>
	/// Represents the credentials for a user trying to authenticate
	/// </summary>
	public class AuthCredentials {
		public string Id { get; set; }
		public string Password { get; set; }
		public override string ToString() {
			return string.Format("Credentials(Username={0})", Id);
		}
	}
}