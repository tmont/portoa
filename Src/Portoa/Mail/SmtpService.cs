using System;
using System.Net.Mail;
using JetBrains.Annotations;

namespace Portoa.Mail {
	/// <summary>
	/// <see cref="IEmailService"/> implementation that uses .NET's <see cref="SmtpClient"/> to
	/// send messages
	/// </summary>
	public class SmtpService : IEmailService {
		private readonly SmtpClient client;

		public SmtpService([NotNull]SmtpClient client) {
			this.client = client;
		}

		public void Send(MailMessage message) {
			try {
				client.Send(message);
			} catch (Exception e) {
				throw new EmailException("Failed to send message", e);
			}
		}
	}
}