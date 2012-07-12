using System.Web.Security;
using Portoa.Security;

namespace Portoa.Web.Security {
	/// <summary>
	/// Provides a mechanism to create a <see cref="FormsAuthenticationTicket"/>
	/// </summary>
	/// <seealso cref="IAuthenticationService"/>
	public interface IAuthTicketFactory {
		FormsAuthenticationTicket Create(string id);
	}
}