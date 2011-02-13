using System;
using System.Security.Cryptography;
using System.Text;

namespace Portoa.Security {
	public static class SecurityExtensions {
		/// <summary>
		/// Computes the <see cref="HMACSHA256"/> hash of the string using the given
		/// salt and <c cref="Encoding.UTF8">UTF8 Encoding</c>
		/// </summary>
		/// <param name="plainText"></param>
		/// <param name="salt"></param>
		/// <returns></returns>
		public static byte[] ComputeHash(this string plainText, string salt) {
			var encoding = Encoding.UTF8;
			using (var sha = new HMACSHA256(Encoding.UTF8.GetBytes(salt))) {
				return sha.ComputeHash(encoding.GetBytes(plainText));
			}
		}

		/// <summary>
		/// Base-64 encodes the byte array
		/// </summary>
		public static string Base64Encode(this byte[] bytes) {
			return Convert.ToBase64String(bytes);
		}

		/// <summary>
		/// Converts a byte array to a hex-encoded string
		/// </summary>
		public static string ToHex(this byte[] bytes) {
			return BitConverter.ToString(bytes).Replace("-", "");
		}

	}
}