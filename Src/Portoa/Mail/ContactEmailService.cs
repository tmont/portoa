using System;
using System.Net.Mail;
using JetBrains.Annotations;

namespace Portoa.Mail {

	/// <summary>
	/// Similar to <see cref="IEmailService"/>, but requires more configuration and less
	/// message building
	/// </summary>
	public interface IContactEmailService {
		/// <summary>
		/// Sends a predefined form email using the settings injected into the implementation. The <c>Reply-To</c>
		/// header will be given the value of <paramref name="fromAddress"/> while <paramref name="fromName"/> will be
		/// the display name.
		/// </summary>
		/// <param name="body">The body of the message</param>
		/// <param name="fromAddress">The address of the user who filled out the contact form</param>
		/// <param name="fromName">The display name of the user who filled out the contact form</param>
		void Send(string body, string fromAddress, string fromName);
	}

	/// <summary>
	/// <see cref="IEmailService"/> composite that allows for convenient contact form notification
	/// </summary>
	public class ContactEmailService : IContactEmailService {
		private readonly IEmailService emailService;
		private readonly ContactEmailSettings settings;

		public ContactEmailService([NotNull]IEmailService emailService, [NotNull]ContactEmailSettings settings) {
			VerifySettings(settings);
			this.emailService = emailService;
			this.settings = settings;
		}

		public void Send(string body, string fromAddress, string fromName) {
			var message = new MailMessage(new MailAddress(settings.FromAddress, settings.FromName), new MailAddress(settings.ToAddress)) {
				Subject = string.Format(settings.SubjectFormat, fromAddress, fromName),
				Body = body ?? string.Empty
			};

			message.ReplyToList.Add(new MailAddress(fromAddress, fromName));

			emailService.Send(message);
		}

		private static void VerifySettings(ContactEmailSettings settings) {
			if (string.IsNullOrEmpty(settings.FromAddress)) {
				throw new ArgumentException("ContactEmailSettings.FromAddress is required");
			}
			if (string.IsNullOrEmpty(settings.ToAddress)) {
				throw new ArgumentException("ContactEmailSettings.ToAddress is required");
			}
		}

		/// <summary>
		/// Represents settings for sending a contact form notification
		/// </summary>
		public struct ContactEmailSettings {
			/// <summary>
			/// The value of the <c>From</c> header
			/// </summary>
			public string FromAddress { get; set; }
			/// <summary>
			/// The display name for the <c>From</c> header
			/// </summary>
			public string FromName { get; set; }
			/// <summary>
			/// A string format for the <c>Subject</c> line; it takes two arguments: the address of the 
			/// user filling out the form and his display name (both provided via user input)
			/// </summary>
			public string SubjectFormat { get; set; }
			/// <summary>
			/// The value of the <c>To</c> header
			/// </summary>
			public string ToAddress { get; set; }
		}
	}
}