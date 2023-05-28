using System;
using System.Text;
using NaCl;
using SmartAssembly.Delegates;
using SmartAssembly.HouseOfCards;

namespace PusherClient
{
	internal sealed class ChannelDataDecrypter : IChannelDataDecrypter
	{
		public string DecryptData(byte[] decryptionKey, EncryptedChannelData encryptedData)
		{
			byte[] array = null;
			byte[] array2 = null;
			if (encryptedData != null)
			{
				if (encryptedData.ciphertext != null)
				{
					array = Convert.FromBase64String(encryptedData.ciphertext);
				}
				if (encryptedData.nonce != null)
				{
					array2 = Convert.FromBase64String(encryptedData.nonce);
				}
			}
			if (array != null && array2 != null)
			{
				using (XSalsa20Poly1305 xsalsa20Poly = new XSalsa20Poly1305(decryptionKey))
				{
					byte[] array3 = new byte[array.Length - 16];
					if (!xsalsa20Poly.TryDecrypt(array3, array, array2))
					{
						throw new ChannelDecryptionException(ChannelDataDecrypter.getString_0(107313190));
					}
					return Encoding.UTF8.GetString(array3);
				}
			}
			throw new ChannelDecryptionException(ChannelDataDecrypter.getString_0(107313149));
		}

		static ChannelDataDecrypter()
		{
			Strings.CreateGetStringDelegate(typeof(ChannelDataDecrypter));
		}

		[NonSerialized]
		internal static GetString getString_0;
	}
}
