using System.Net.Mail;
using JetBrains.Annotations;

namespace Portoa.Mail {
	public interface IEmailService {
		/// <summary>
		/// Sends an email <paramref name="message"/>
		/// </summary>
		/// <param name="message">The message to send</param>
		/// <exception cref="EmailException">Raised if an error occurred while sending the message</exception>
		void Send([NotNull]MailMessage message);
	}
}