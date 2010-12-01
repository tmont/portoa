using System;
using System.Security.Cryptography;
using System.Text;

namespace Portoa.Security {
	public static class SecurityExtensions {
		public static byte[] ComputeHash(this string plainText, string salt) {
			var encoding = Encoding.UTF8;
			using (var sha = new HMACSHA256(Encoding.UTF8.GetBytes(salt))) {
				return sha.ComputeHash(encoding.GetBytes(plainText));
			}
		}

		public static string Base64Encode(this byte[] bytes) {
			return Convert.ToBase64String(bytes);
		}

		public static string ToHex(this byte[] bytes) {
			return BitConverter.ToString(bytes).Replace("-", "");
		}

	}
}