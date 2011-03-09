using System;
using System.Linq;
using NUnit.Framework;
using Portoa.Security;

namespace Portoa.Tests.Security {
	[TestFixture]
	public class CaptchaTests {

		private CaptchaManager captchaManager;

		[SetUp]
		public void SetUp() {
			captchaManager = new CaptchaManager(new[] { "foo", "bar", "baz", "bat" });
		}

		[Test]
		public void Should_get_random_answer_and_match() {
			var answer = captchaManager.GetRandomAnswer();
			Assert.That(captchaManager.IsMatch(answer.HashedAnswer, answer.Answer), Is.True);
		}

		[Test]
		public void Should_not_match_when_answer_does_not_exist() {
			var answer = captchaManager.GetRandomAnswer();
			Assert.That(captchaManager.IsMatch(answer.HashedAnswer, "not correct"), Is.False);
		}

		[Test]
		public void Should_not_match_when_answer_does_not_match() {
			Assert.That(captchaManager.IsMatch("foo", "foo"), Is.False);
		}

		[Test, ExpectedException(typeof(ArgumentException))]
		public void Should_require_at_least_one_answer() {
			new CaptchaManager(Enumerable.Empty<string>());
		}
	}
}