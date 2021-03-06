﻿using System.Web.Security;
using Portoa.Security;

namespace Portoa.Web.Security {
	/// <summary>
	/// Base class for authenticating users using the built-in <see cref="FormsAuthentication"/>
	/// mechanism
	/// </summary>
	public class FormsAuthenticationServiceBase : IAuthenticationService {
		private readonly IAuthenticator authenticator;

		public FormsAuthenticationServiceBase(IAuthenticator authenticator) {
			this.authenticator = authenticator;
		}

		void IAuthenticationService.Login(string username) {
			FormsAuthentication.SetAuthCookie(username, true);
			Login(username);
		}

		void IAuthenticationService.Logout() {
			FormsAuthentication.SignOut();
			Logout();
		}

		/// <summary>
		/// Performs any extra logout tasks; default implementation does nothing
		/// </summary>
		protected virtual void Logout() { }

		/// <summary>
		/// Performs any extra login tasks; default implementation does nothing
		/// </summary>
		/// <param name="username">The name of the user to login</param>
		protected virtual void Login(string username) { }

		bool IAuthenticator.IsValid(AuthCredentials credentials) {
			return authenticator.IsValid(credentials);
		}
	}
}