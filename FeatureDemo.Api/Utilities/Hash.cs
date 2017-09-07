using System;
using System.Security.Cryptography;
using System.Text;

namespace FeatureDemo.Api.Utilities
{
    public static class Hash
    {
		public static string SHA1HashStringForUTF8String(string s)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(s);

			var sha1 = SHA1.Create();
			byte[] hashBytes = sha1.ComputeHash(bytes);

			return HexStringFromBytes(hashBytes);
		}

		public static string HexStringFromBytes(byte[] bytes)
		{
			var sb = new StringBuilder();
			foreach (byte b in bytes)
			{
				var hex = b.ToString("X2");
				sb.Append(hex);
			}
			return sb.ToString();
		}
    }
}
