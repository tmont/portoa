using System;
using System.Net.Mail;
using Moq;
using NUnit.Framework;
using Portoa.Mail;

namespace Portoa.Tests.Mail {
	[TestFixture]
	public class ContactEmailServiceTests {
		[Test]
		public void Should_send_contact_email_through_injected_service() {
			var emailService = new Mock<IEmailService>();
			emailService
				.Setup(service => service.Send(It.IsAny<MailMessage>()))
				.Callback<MailMessage>(message => {
					Assert.That(message.Subject, Is.EqualTo("[contact] Message from foo@bar.com (Foo Bar)"));
					Assert.That(message.To, Has.Count.EqualTo(1));
					Assert.That(message.To[0].Address, Is.EqualTo("admin@example.com"));
					Assert.That(message.From.Address, Is.EqualTo("contact-no-reply@example.com"));
					Assert.That(message.From.DisplayName, Is.EqualTo("Contact Bot"));
					Assert.That(message.ReplyToList, Has.Count.EqualTo(1));
					Assert.That(message.ReplyToList[0].Address, Is.EqualTo("foo@bar.com"));
					Assert.That(message.ReplyToList[0].DisplayName, Is.EqualTo("Foo Bar"));
					Assert.That(message.Body, Is.EqualTo("body"));
				}).Verifiable();

			var settings = new ContactEmailService.ContactEmailSettings {
				FromAddress = "contact-no-reply@example.com",
				FromName = "Contact Bot",
				SubjectFormat = "[contact] Message from {0} ({1})",
				ToAddress = "admin@example.com"
			};

			var contactService = new ContactEmailService(emailService.Object, settings);
			contactService.Send("body", "foo@bar.com", "Foo Bar");
			emailService.VerifyAll();
		}

		[Test, ExpectedException(typeof(ArgumentException), ExpectedMessage = "ContactEmailSettings.FromAddress is required")]
		public void Should_require_from_address() {
			new ContactEmailService(new Mock<IEmailService>().Object, new ContactEmailService.ContactEmailSettings { ToAddress = "foo@bar.com" });
		}

		[Test, ExpectedException(typeof(ArgumentException), ExpectedMessage = "ContactEmailSettings.ToAddress is required")]
		public void Should_require_to_address() {
			new ContactEmailService(new Mock<IEmailService>().Object, new ContactEmailService.ContactEmailSettings { FromAddress = "foo@bar.com" });
		}
	}
}