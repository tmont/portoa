using System.Web;
using System.Web.Security;
using JetBrains.Annotations;
using Portoa.Security;

namespace Portoa.Web.Security {
	/// <summary>
	/// Authenticates users via a <see cref="FormsAuthenticationTicket"/>.
	/// The ticket is stored in the cookie defined by <see cref="FormsAuthentication.FormsCookieName"/>.
	/// </summary>
	public class AuthTicketService : IAuthenticationService {
		private readonly HttpContextBase httpContext;
		private readonly IAuthTicketFactory ticketFactory;
		private readonly IAuthenticator authenticator;

		public AuthTicketService(
			[NotNull]HttpContextBase httpContext,
			[NotNull]IAuthTicketFactory ticketFactory,
			[NotNull]IAuthenticator authenticator) {
			this.httpContext = httpContext;
			this.ticketFactory = ticketFactory;
			this.authenticator = authenticator;
		}

		public void Login(string username) {
			httpContext.Response.Cookies.Add(new HttpCookie(
				FormsAuthentication.FormsCookieName, 
				FormsAuthentication.Encrypt(ticketFactory.Create(username))
			));
		}

		public void Logout() {
			FormsAuthentication.SignOut();
		}

		public bool IsValid(AuthCredentials credentials) {
			return authenticator.IsValid(credentials);
		}
	}
}