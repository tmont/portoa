using Portoa.Security;

namespace Portoa.Web.Models {
	/// <summary>
	/// Represents a model to be used to verify that the user is not a spambot
	/// </summary>
	public interface ICaptchaModel {
		/// <summary>
		/// The answer to the captcha question supplied by the user
		/// </summary>
		string CaptchaAnswer { get; set; }
		/// <summary>
		/// The hashed version of the captcha answer
		/// </summary>
		/// <see cref="CaptchaManager.IsMatch"/>
		string HashedCaptchaAnswer { get; set; }
		/// <summary>
		/// The unhashed, plaintext version of the captcha answer, to be sent from the server
		/// to the client to construct an appropriate view (this value should not be sent back
		/// from the client, obviously)
		/// </summary>
		string UnhashedCaptchaAnswer { get; set; }
	}
}