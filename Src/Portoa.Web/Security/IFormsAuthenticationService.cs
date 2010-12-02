using Portoa.Logging;

namespace Portoa.Web.Security {
	public interface IFormsAuthenticationService {
		void Login(string username, bool createPersistentCookie);
		void Logout();
		bool IsValid(string username, [DoNotLog]string password);
	}
}