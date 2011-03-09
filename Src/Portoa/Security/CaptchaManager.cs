using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Portoa.Util;

namespace Portoa.Security {
	/// <summary>
	/// <para> Manages answers to captcha questions. Technically, it's not actually captcha, but rather
	/// just a means to prevent spambots.</para>
	/// <para>Essentially, you give this object a collection of plaintext answers to questions that you
	/// require a user to answer before submitting something. This class will use those answers to verify
	/// whether the questions were answered correctly.</para>
	/// </summary>
	public class CaptchaManager {
		private static readonly Random random = new Random();
		private readonly IDictionary<string, string> answers = new Dictionary<string, string>();

		/// <param name="plaintextAnswers">A collection of plaintext answers that the user is expected to provide when prompted</param>
		public CaptchaManager(IEnumerable<string> plaintextAnswers) {
			if (!plaintextAnswers.Any()) {
				throw new ArgumentException("At least one answer is required");
			}

			using (var hasher = MD5.Create()) {
				plaintextAnswers.Walk(answer => answers.Add(answer, Convert.ToBase64String(hasher.ComputeHash(Encoding.ASCII.GetBytes(answer)))));
			}
		}

		/// <summary>
		/// Gets a random answer from the user-defined collection of answers
		/// </summary>
		public CaptchaAnswer GetRandomAnswer() {
			var answer = answers.Keys.ToArray()[random.Next(answers.Count)];
			return new CaptchaAnswer {
				Answer = answer,
				HashedAnswer = answers[answer]
			};
		}

		/// <summary>
		/// Determines whether the potential answer in plaintext matches the hashed answer
		/// </summary>
		/// <param name="hashedAnswer">The hashed answer</param>
		/// <param name="potentialPlaintextAnswer">The uncertain answer in plaintext</param>
		/// <returns><c>true</c> if the answer is correct, <c>false</c> if the answer does not exist or it doesn't match the hashed answer</returns>
		public bool IsMatch(string hashedAnswer, string potentialPlaintextAnswer) {
			if (!answers.ContainsKey(potentialPlaintextAnswer)) {
				return false;
			}

			return answers[potentialPlaintextAnswer] == hashedAnswer;
		}

		/// <summary>
		/// Represents a single answer to a captcha question
		/// </summary>
		public struct CaptchaAnswer {
			/// <summary>
			/// The answer in plaintext
			/// </summary>
			public string Answer { get; set; }
			/// <summary>
			/// The answers hashed and converted to base64
			/// </summary>
			public string HashedAnswer { get; set; }
		}
	}
}