namespace Portoa.Security {
	public interface IPasswordProtected {
		bool VerifyPassword(string password);
		void ChangePassword(string newPassword);
	}
}